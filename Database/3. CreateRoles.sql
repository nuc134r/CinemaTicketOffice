SET NOCOUNT ON;

IF DATABASE_PRINCIPAL_ID('greenbird_user') IS NULL
CREATE ROLE greenbird_user

IF DATABASE_PRINCIPAL_ID('greenbird_admin') IS NULL
CREATE ROLE greenbird_admin

IF DATABASE_PRINCIPAL_ID('greenbird_superadmin') IS NULL
CREATE ROLE greenbird_superadmin