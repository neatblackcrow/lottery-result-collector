
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/11/2018 01:22:48
-- Generated from EDMX file: D:\Documents\Visual Studio 2017\Projects\LottoResult\LotteryResult\LotteryResult\Models\LotteryResult.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LottoResult];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_result_to_reward_type]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[result] DROP CONSTRAINT [FK_result_to_reward_type];
GO
IF OBJECT_ID(N'[dbo].[FK_result_to_round]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[result] DROP CONSTRAINT [FK_result_to_round];
GO
IF OBJECT_ID(N'[dbo].[FK_reward_type_to_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[reward_type] DROP CONSTRAINT [FK_reward_type_to_user];
GO
IF OBJECT_ID(N'[dbo].[FK_user_to_role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[user] DROP CONSTRAINT [FK_user_to_role];
GO
IF OBJECT_ID(N'[dbo].[FK_round_to_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[round] DROP CONSTRAINT [FK_round_to_user];
GO
IF OBJECT_ID(N'[dbo].[FK_result_data_to_result]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[result_data] DROP CONSTRAINT [FK_result_data_to_result];
GO
IF OBJECT_ID(N'[dbo].[FK_result_data_to_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[result_data] DROP CONSTRAINT [FK_result_data_to_user];
GO
IF OBJECT_ID(N'[dbo].[FK_ad_message_to_round]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ad_message] DROP CONSTRAINT [FK_ad_message_to_round];
GO
IF OBJECT_ID(N'[dbo].[FK_ad_message_to_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ad_message] DROP CONSTRAINT [FK_ad_message_to_user];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[result]', 'U') IS NOT NULL
    DROP TABLE [dbo].[result];
GO
IF OBJECT_ID(N'[dbo].[reward_type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[reward_type];
GO
IF OBJECT_ID(N'[dbo].[role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[role];
GO
IF OBJECT_ID(N'[dbo].[round]', 'U') IS NOT NULL
    DROP TABLE [dbo].[round];
GO
IF OBJECT_ID(N'[dbo].[user]', 'U') IS NOT NULL
    DROP TABLE [dbo].[user];
GO
IF OBJECT_ID(N'[dbo].[result_data]', 'U') IS NOT NULL
    DROP TABLE [dbo].[result_data];
GO
IF OBJECT_ID(N'[dbo].[ad_message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ad_message];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'result'
CREATE TABLE [dbo].[result] (
    [id] int IDENTITY(1,1) NOT NULL,
    [round_id] int  NOT NULL,
    [reward_type_id] int  NOT NULL,
    [reward_order] int  NOT NULL,
    [result_order] int  NOT NULL
);
GO

-- Creating table 'reward_type'
CREATE TABLE [dbo].[reward_type] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL,
    [instance] int  NOT NULL,
    [format] varchar(50)  NOT NULL,
    [reward_amount] varchar(50)  NOT NULL,
    [create_timestamp] datetime  NOT NULL,
    [create_by] int  NOT NULL,
    [is_active] bit  NOT NULL
);
GO

-- Creating table 'role'
CREATE TABLE [dbo].[role] (
    [id] tinyint IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'round'
CREATE TABLE [dbo].[round] (
    [id] int IDENTITY(1,1) NOT NULL,
    [date] datetime  NOT NULL,
    [round1] int  NOT NULL,
    [create_by] int  NOT NULL,
    [create_timestamp] datetime  NOT NULL
);
GO

-- Creating table 'user'
CREATE TABLE [dbo].[user] (
    [id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(50)  NOT NULL,
    [hashed_password] varchar(max)  NOT NULL,
    [old_password] varchar(max)  NULL,
    [firstname] nvarchar(50)  NOT NULL,
    [lastname] nvarchar(50)  NOT NULL,
    [role_id] tinyint  NOT NULL,
    [create_timestamp] datetime  NOT NULL,
    [is_active] bit  NOT NULL
);
GO

-- Creating table 'result_data'
CREATE TABLE [dbo].[result_data] (
    [result] varchar(50)  NOT NULL,
    [create_timestamp] datetime  NOT NULL,
    [create_by] int  NOT NULL,
    [result_id] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [is_confirmed_result] bit  NOT NULL
);
GO

-- Creating table 'ad_message'
CREATE TABLE [dbo].[ad_message] (
    [id] int IDENTITY(1,1) NOT NULL,
    [advertise_msg] nvarchar(250)  NOT NULL,
    [create_by] int  NOT NULL,
    [create_timestamp] datetime  NOT NULL,
    [round_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'result'
ALTER TABLE [dbo].[result]
ADD CONSTRAINT [PK_result]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'reward_type'
ALTER TABLE [dbo].[reward_type]
ADD CONSTRAINT [PK_reward_type]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'role'
ALTER TABLE [dbo].[role]
ADD CONSTRAINT [PK_role]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'round'
ALTER TABLE [dbo].[round]
ADD CONSTRAINT [PK_round]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'user'
ALTER TABLE [dbo].[user]
ADD CONSTRAINT [PK_user]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'result_data'
ALTER TABLE [dbo].[result_data]
ADD CONSTRAINT [PK_result_data]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ad_message'
ALTER TABLE [dbo].[ad_message]
ADD CONSTRAINT [PK_ad_message]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [reward_type_id] in table 'result'
ALTER TABLE [dbo].[result]
ADD CONSTRAINT [FK_result_to_reward_type]
    FOREIGN KEY ([reward_type_id])
    REFERENCES [dbo].[reward_type]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_result_to_reward_type'
CREATE INDEX [IX_FK_result_to_reward_type]
ON [dbo].[result]
    ([reward_type_id]);
GO

-- Creating foreign key on [round_id] in table 'result'
ALTER TABLE [dbo].[result]
ADD CONSTRAINT [FK_result_to_round]
    FOREIGN KEY ([round_id])
    REFERENCES [dbo].[round]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_result_to_round'
CREATE INDEX [IX_FK_result_to_round]
ON [dbo].[result]
    ([round_id]);
GO

-- Creating foreign key on [create_by] in table 'reward_type'
ALTER TABLE [dbo].[reward_type]
ADD CONSTRAINT [FK_reward_type_to_user]
    FOREIGN KEY ([create_by])
    REFERENCES [dbo].[user]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_reward_type_to_user'
CREATE INDEX [IX_FK_reward_type_to_user]
ON [dbo].[reward_type]
    ([create_by]);
GO

-- Creating foreign key on [role_id] in table 'user'
ALTER TABLE [dbo].[user]
ADD CONSTRAINT [FK_user_to_role]
    FOREIGN KEY ([role_id])
    REFERENCES [dbo].[role]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_user_to_role'
CREATE INDEX [IX_FK_user_to_role]
ON [dbo].[user]
    ([role_id]);
GO

-- Creating foreign key on [create_by] in table 'round'
ALTER TABLE [dbo].[round]
ADD CONSTRAINT [FK_round_to_user]
    FOREIGN KEY ([create_by])
    REFERENCES [dbo].[user]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_round_to_user'
CREATE INDEX [IX_FK_round_to_user]
ON [dbo].[round]
    ([create_by]);
GO

-- Creating foreign key on [result_id] in table 'result_data'
ALTER TABLE [dbo].[result_data]
ADD CONSTRAINT [FK_result_data_to_result]
    FOREIGN KEY ([result_id])
    REFERENCES [dbo].[result]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_result_data_to_result'
CREATE INDEX [IX_FK_result_data_to_result]
ON [dbo].[result_data]
    ([result_id]);
GO

-- Creating foreign key on [create_by] in table 'result_data'
ALTER TABLE [dbo].[result_data]
ADD CONSTRAINT [FK_result_data_to_user]
    FOREIGN KEY ([create_by])
    REFERENCES [dbo].[user]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_result_data_to_user'
CREATE INDEX [IX_FK_result_data_to_user]
ON [dbo].[result_data]
    ([create_by]);
GO

-- Creating foreign key on [round_id] in table 'ad_message'
ALTER TABLE [dbo].[ad_message]
ADD CONSTRAINT [FK_ad_message_to_round]
    FOREIGN KEY ([round_id])
    REFERENCES [dbo].[round]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ad_message_to_round'
CREATE INDEX [IX_FK_ad_message_to_round]
ON [dbo].[ad_message]
    ([round_id]);
GO

-- Creating foreign key on [create_by] in table 'ad_message'
ALTER TABLE [dbo].[ad_message]
ADD CONSTRAINT [FK_ad_message_to_user]
    FOREIGN KEY ([create_by])
    REFERENCES [dbo].[user]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ad_message_to_user'
CREATE INDEX [IX_FK_ad_message_to_user]
ON [dbo].[ad_message]
    ([create_by]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------