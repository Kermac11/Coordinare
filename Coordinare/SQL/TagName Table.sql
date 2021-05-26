CREATE TABLE [dbo].[TagNames] (
    [Tag_ID]  INT          IDENTITY (1, 1) NOT NULL,
    [TagName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tag_ID] PRIMARY KEY CLUSTERED ([Tag_ID] ASC)
);