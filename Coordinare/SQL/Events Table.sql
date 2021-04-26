CREATE TABLE [dbo].[Events] (
    [Event_ID]  INT          IDENTITY (1, 1) NOT NULL,
    [Duration]  VARCHAR (50) NULL,
    [Room_ID]   VARCHAR (50) NULL,
    [EventName] VARCHAR (50) NULL,
    [DateTime]  DATETIME     NOT NULL,
    [Eventinfo] VARCHAR (50) NULL,
    [SS_Amount] INT          NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Event_ID] ASC),
    CONSTRAINT [FK_Events_Rooms] FOREIGN KEY ([Room_ID]) REFERENCES [dbo].[Rooms] ([Room_ID]) ON UPDATE CASCADE
);

