

if not exists (select * from sys.objects where type = 'P' and name = 'GenerateCounterForServices')
begin
    exec('
    CREATE PROCEDURE GenerateCounterForServices
        @ReservationDate DATE,
        @OrgId INT,
        @QGID INT,
        @Counter INT OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON;
        BEGIN TRANSACTION;
        SELECT @Counter = COUNT(*)
        FROM Reservations WITH (ROWLOCK, UPDLOCK)
        WHERE CAST(ReservationDate AS DATE) = @ReservationDate
          AND OrgId = @OrgId
          AND ServiceId = @QGID;
        SET @Counter = @Counter + 1;
        COMMIT TRANSACTION;
    END;
    ');
end

go

alter PROCEDURE GenerateCounterForServices
    @ReservationDate DATE,
    @OrgId INT,
    @QGID INT,
    @Counter INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    SELECT @Counter = COUNT(*)
    FROM Reservations WITH (ROWLOCK, UPDLOCK)
    WHERE CAST(ReservationDate AS DATE) = @ReservationDate
      AND OrgId = @OrgId
      AND ServiceId = @QGID;

    SET @Counter = @Counter + 1;

    COMMIT TRANSACTION;
END;
