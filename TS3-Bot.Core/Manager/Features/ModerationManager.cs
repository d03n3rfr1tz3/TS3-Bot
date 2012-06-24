namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using Base;
    using Repository;

    public class ModerationManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="ModerationManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ModerationManager(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Determines whether this instance [can slow invoke].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can slow invoke]; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanSlowInvoke()
        {
            return true;
        }

        /// <summary>
        /// Slows the invoke.
        /// </summary>
        public override void SlowInvoke()
        {
            //TODO: Query Log and parse Entries:
            // client (id:X) was added to servergroup 'XX'(id:X) by client 'XX'(id:X)
            // client (id:X) was removed from servergroup 'XX'(id:9) by client 'XX'(id:X)
            //QueryRunner.GetLogEntries(100);
        }

        #endregion

        #region Protected Methods

        #endregion
    }
}