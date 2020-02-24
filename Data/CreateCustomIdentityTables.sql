CREATE TABLE [dbo].[CustomUser] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Email]              NVARCHAR (256)   NULL,
    [EmailConfirmed]     BIT              NOT NULL,
    [PasswordHash]       NVARCHAR (MAX)   NULL,
    [UserName]           NVARCHAR (256)   NOT NULL,
    [NormalizedUserName] NCHAR (256)      NULL,
    CONSTRAINT [PK_dbo.CustomUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[CustomUser]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);



CREATE TABLE [dbo].[CustomRole] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] VARCHAR (256)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[CustomUserRole]
(
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [RoleId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_CustomUserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_CustomUserRole_CustomRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[CustomRole] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomUserRole_CustomUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[CustomUser] ([Id]) ON DELETE CASCADE
)

GO
CREATE NONCLUSTERED INDEX [IX_CustomUserRole_RoleId]
    ON [dbo].[CustomUserRole]([RoleId] ASC);


