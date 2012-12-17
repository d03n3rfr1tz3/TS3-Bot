namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System;
    using System.IO;
    using System.Linq;
    using Entity;
    using Helper;
    using Microsoft.Isam.Esent.Collections.Generic;
    using Service;

    public class DataMigrator
    {
        internal static readonly object MigrateAwayLock = new object();
        internal static readonly object MigrateStickyLock = new object();
        internal static readonly object MigrateVotedLock = new object();
        internal static readonly object MigrateSeenLock = new object();
        internal static readonly object MigrateModeratedLock = new object();
        internal static readonly object MigrateModeratesLock = new object();
        internal static readonly object MigrateTimeLock = new object();
        internal static readonly object MigrateTimesLock = new object();
        internal static readonly object MigratePrevServerGroupsLock = new object();

        public static void MigrateAll(string name)
        {
            MigrateAway(name);
            MigrateSticky(name);
            MigrateVoted(name);
            MigrateSeen(name);
            MigrateModerated(name);
            MigrateModerates(name);
            MigrateTime(name);
            MigrateTimes(name);
            MigratePrevServerGroups(name);
        }

        public static void MigrateAway(string name)
        {
            lock (MigrateAwayLock)
            {
                var directory = string.Format(@"{0}\{1}\Away", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'Away' data.");

                    var oldDictionary = new PersistentDictionary<uint, AwayClientEntity>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            database.Away.AddObject(new Away
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)oldDictionaryEntry.Key,
                                LastChannelId = (int)oldDictionaryEntry.Value.LastChannelId,
                                Creation = oldDictionaryEntry.Value.Creation
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'Away' data.");
                }
            }
        }

        public static void MigrateSticky(string name)
        {
            lock (MigrateStickyLock)
            {
                var directory = string.Format(@"{0}\{1}\Sticky", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'Sticky' data.");

                    var oldDictionary = new PersistentDictionary<Guid, StickyClientEntity>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            database.Sticky.AddObject(new Sticky
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)oldDictionaryEntry.Value.ClientDatabaseId,
                                ChannelId = (int)oldDictionaryEntry.Value.ChannelId,
                                StickTime = (int)oldDictionaryEntry.Value.StickTime,
                                Creation = oldDictionaryEntry.Value.Creation
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'Sticky' data.");
                }
            }
        }

        public static void MigrateVoted(string name)
        {
            lock (MigrateVotedLock)
            {
                var directory = string.Format(@"{0}\{1}\Voted", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'Voted' data.");

                    var oldDictionary = new PersistentDictionary<Guid, VotedClientEntity>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            database.Vote.AddObject(new Vote
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)oldDictionaryEntry.Value.ClientDatabaseId,
                                ChannelId = (int?)oldDictionaryEntry.Value.ChannelId,
                                Creation = oldDictionaryEntry.Value.Creation
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'Voted' data.");
                }
            }
        }

        public static void MigrateSeen(string name)
        {
            lock (MigrateSeenLock)
            {
                var directory = string.Format(@"{0}\{1}\Seen", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'Seen' data.");

                    var oldDictionary = new PersistentDictionary<uint, DateTime>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            database.Seen.AddObject(new Seen
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)oldDictionaryEntry.Key,
                                LastSeen = oldDictionaryEntry.Value
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'Seen' data.");
                }
            }
        }

        public static void MigrateModerated(string name)
        {
            lock (MigrateModeratedLock)
            {
                var directory = string.Format(@"{0}\{1}\Moderated", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'Moderated' data.");

                    var oldDictionary = new PersistentDictionary<Guid, ModeratedClientEntity>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            database.Moderate.AddObject(new Moderate
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)oldDictionaryEntry.Value.User,
                                ModeratorDatabaseId = (int)oldDictionaryEntry.Value.Moderator,
                                ServerGroup = (int)oldDictionaryEntry.Value.ServerGroup,
                                Type = (byte)oldDictionaryEntry.Value.Type,
                                Creation = oldDictionaryEntry.Value.Moderated
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'Moderated' data.");
                }
            }
        }

        public static void MigrateModerates(string name)
        {
            lock (MigrateModeratesLock)
            {
                var directory = string.Format(@"{0}\{1}\Moderates", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'Moderates' data.");

                    var oldDictionary = new PersistentDictionary<string, ModeratedClientEntity>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            database.Moderate.AddObject(new Moderate
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)oldDictionaryEntry.Value.User,
                                ModeratorDatabaseId = (int)oldDictionaryEntry.Value.Moderator,
                                ServerGroup = (int)oldDictionaryEntry.Value.ServerGroup,
                                Type = (byte)oldDictionaryEntry.Value.Type,
                                Creation = oldDictionaryEntry.Value.Moderated
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'Moderates' data.");
                }
            }
        }
        
        public static void MigrateTime(string name)
        {
            lock (MigrateTimeLock)
            {
                var directory = string.Format(@"{0}\{1}\Time", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'Time' data.");

                    var oldDictionary = new PersistentDictionary<Guid, TimeClientEntity>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            database.Time.AddObject(new Time
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)oldDictionaryEntry.Value.User,
                                Joined = oldDictionaryEntry.Value.Joined,
                                Disconnected = oldDictionaryEntry.Value.Disconnected
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'Time' data.");
                }
            }
        }

        public static void MigrateTimes(string name)
        {
            lock (MigrateTimesLock)
            {
                var directory = string.Format(@"{0}\{1}\Times", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'Times' data.");

                    var oldDictionary = new PersistentDictionary<string, TimeClientEntity>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            database.Time.AddObject(new Time
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)oldDictionaryEntry.Value.User,
                                Joined = oldDictionaryEntry.Value.Joined,
                                Disconnected = oldDictionaryEntry.Value.Disconnected
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'Times' data.");
                }
            }
        }

        public static void MigratePrevServerGroups(string name)
        {
            lock (MigratePrevServerGroupsLock)
            {
                var directory = string.Format(@"{0}\{1}\PrevServerGroups", BasicHelper.DataDirectory, name);
                if (PersistentDictionaryFile.Exists(directory))
                {
                    LogService.Debug("Start migrating 'PrevServerGroups' data.");

                    var oldDictionary = new PersistentDictionary<uint, string>(directory);

                    using (var database = new BotDatabaseEntities())
                    {
                        foreach (var oldDictionaryEntry in oldDictionary)
                        {
                            oldDictionaryEntry.Value.Split(';').Select(int.Parse).ForEach(oldServerGroup =>
                            {
                                database.PreviousServerGroup.AddObject(new PreviousServerGroup
                                {
                                    Id = Guid.NewGuid(),
                                    ClientDatabaseId = (int)oldDictionaryEntry.Key,
                                    ServerGroup = oldServerGroup
                                });
                            });
                        }

                        database.SaveChanges();
                    }

                    oldDictionary.Flush();
                    oldDictionary.Dispose();

                    PersistentDictionaryFile.DeleteFiles(directory);
                    Directory.Delete(directory);

                    LogService.Debug("Finished migrating 'PrevServerGroups' data.");
                }
            }
        }
    }
}