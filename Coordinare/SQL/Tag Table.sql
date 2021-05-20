CREATE TABLE [dbo].[Tags] (
    [Tag_ID]   INT          IDENTITY (1, 1) NOT NULL,
    [Event_ID] INT          NOT NULL,
    [TagName]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tag_ID] PRIMARY KEY CLUSTERED ([Tag_ID] ASC)
);

