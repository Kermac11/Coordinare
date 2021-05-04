CREATE TABLE [dbo].[Events] (
    [Event_ID]  INT          IDENTITY (1, 1) NOT NULL,
    [Duration]  BIGINT       NOT NULL,
    [Speaker]   INT          NOT NULL,
    [Room_ID]   VARCHAR (50) NULL,
    [EventName] VARCHAR (50) NOT NULL,
    [DateTime]  DATETIME     NOT NULL,
    [Eventinfo] VARCHAR (50) NULL,
    [SS_Amount] INT          NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Event_ID] ASC),
    CONSTRAINT [FK_Events_Rooms] FOREIGN KEY ([Room_ID]) REFERENCES [dbo].[Rooms] ([Room_ID]),
    CONSTRAINT [FK_Events_Speaker] FOREIGN KEY ([Speaker]) REFERENCES [dbo].[Users] ([User_ID])
);




