CREATE TABLE [dbo].[Tags] (
    [Tagged_ID] INT IDENTITY (1, 1) NOT NULL,
    [Tag_ID]    INT NOT NULL,
    [Event_ID]  INT NOT NULL,
    CONSTRAINT [PK_Tagged_ID] PRIMARY KEY CLUSTERED ([Tagged_ID] ASC),
    CONSTRAINT [FK_Tags_Tag_ID] FOREIGN KEY ([Tag_ID]) REFERENCES [dbo].[TagNames] ([Tag_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Tags_Events_ID] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([Event_ID]) ON DELETE CASCADE
);

