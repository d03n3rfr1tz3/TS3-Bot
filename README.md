<font size="2" style="font-size: 11pt">**Requirements**</font>

-   <font size="2">Windows XP or higher</font>

-   <font size="2">.NET Framework 4 or higher</font>

<font size="2" style="font-size: 11pt">**Installation**</font>

-   <font size="2">Just click you through the Setup or extract the
    ?Console? folder in any directory you like</font>

-   <font size="2">Make sure the windows service ?TeamSpeak 3 Bot? is
    running, if you choosed the Setup variant</font>

<font size="2" style="font-size: 11pt">**Configuration**</font>

-   <font size="2"><span lang>Open the Configuration in the
    ?Configuration? folder, which is in the Installation folder  
    (default is "</span></font><font size="2"><span lang>*C:\\Program
    Files\\Dirk Sarodnick\\TeamSpeak 3
    Bot\\Configuration\\")*</span></font>

-   <font size="2">Copy one of the example Configurations</font>

-   <font size="2">Open it for editing, so that we can get your Bot
    working</font>

-   <font size="2">Search for the \<TeamSpeak\>-Node in which you can
    configure your Connection.  
    \<Host\> - The IP or Hostname of your server (default: localhost)  
    \<Instance\> - The instance ID of your virtual TeamSpeak server
    (default: 1)  
    \<QueryPort\> - The query port configured in TeamSpeak configuration
    (default: 10011)  
    \<Username\> - username of the ServerQuery user, that the bot will
    representate  
    \<Password\> - password for that ServerQuery user  
    \<GuestGroups\> - A list of \<Group\>-Nodes, were you define the
    Guest groups, which will filter some logical things</font>

-   <font size="2">Then just search for the first \<Enabled\>-Node in
    your Configuration and set the value to ?true? instead of ?false?.
    More details are described in the Configuration itself.</font>

<font size="2">Now your bot is configured and will automatically try to
connect to your TeamSpeak server. The bot will not be visible for you as
a normal connecting user, so don?t panic. Just type !help into the
global chat and the bot should answer you with a private message. Look
into the Troubleshooting section if anything goes wrong here.</font>

<font size="2">Make sure you add the IP on which the Bot is running into
your ?query\_ip\_whitelist.txt? file of your TeamSpeak server. Otherwise
the Bot will be banned after a few seconds from the antispam
mechanism.</font>

<font size="2" style="font-size: 11pt">**Troubleshooting**</font>

<font size="2">The Bot is a service, so you don?t see a black console
with some debug or error information. Of course, there is a log! Because
we are in the windows world, the bot is logging into the windows event
log. You find this under your ?Control Panel? \> ?Administrative Tools?
\> ?Event Viewer?. Just look into the ?Application Log? (in Windows 7/
Windows 2008 under ?Windows Protocols? \> ?Application?) and there you
find a lot of information. The Bot logs with the Source ?TS3-Bot? for
which you can filter. In most cases there are connection problems or
corrupt configurations, which you can identify here.</font>

<font size="2">It?s also possible to start the bot with the console-app
?TS3-Bot.Console.exe?. Here are all errors additionally visible in the
pretty black window.</font>

<font size="2">Do not hesitate to contact me, if you can?t get it to
work. Just email me, but don?t forget to add some details and all recent
log entries from the TS3-Bot.</font>

<font size="2" style="font-size: 11pt">**Contact**</font>

<font size="2">Dirk Sarodnick</font>

<font size="2">webmaster@d03n3r.de</font>

<font size="2">http://www.d03n3r.de</font>

<font size="2" style="font-size: 11pt">**Copyright**</font>

<font size="2">This program is free for use, but please notify me if you
found a bug or if you have some suggestion. The author of this program
is not responsible for any damage or data loss. It is not allowed to
sell this program for money, it has to be free to get. It is also not
allowed to use my source code and publish that with your name. Please
ask me for a permission first if you want to publish something which
contains parts of my source code.</font>

<font size="2"><span lang>Teamspeak 3 is developed by TeamSpeak Systems
GmbH, Sales & Licensing by Triton CI & Associates, Inc. More
informations about Teamspeak 3:</span></font> http://www.teamspeak.com