CREATE TABLE [dbo].[Users] (
    [User_ID]     INT          NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [Username]    VARCHAR (50) NOT NULL,
    [Password]    VARCHAR (50) NOT NULL,
    [Phone]       VARCHAR (50) NULL,
    [Email]       VARCHAR (50) NOT NULL,
    [Speaker]     TINYINT      NOT NULL,
    [Specialaid] TINYINT      NOT NULL,
    PRIMARY KEY CLUSTERED ([User_ID] ASC)
);

