CREATE TABLE [dbo].[Users] (
    [User_ID]    INT          IDENTITY (1, 1)  NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [Username]    VARCHAR (50) NOT NULL,
    [Password]    VARCHAR (50) NOT NULL,
    [Phone]       VARCHAR (50) NULL,
    [Email]       VARCHAR (50) NOT NULL,
    [Speaker]     BIT      NOT NULL,
    [Specialaid] BIT      NOT NULL,
    PRIMARY KEY CLUSTERED ([User_ID] ASC)
);

