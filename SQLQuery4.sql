--Read All Employees
CREATE PROCEDURE [GET_EMPLOYEES]
AS
BEGIN
SELECT Id, FirstName, LastName, DateofBirth, Email, Salary FROM EMPLOYEES WITH (NOLOCK)
   --SELECT * FROM EMPLOYEES WITH (NOLOCK)
END

--GET BY ID 
CREATE PROCEDURE [GET_EMPLOYEEBYID]
(
    @Id INT
)
AS
BEGIN
   SELECT * FROM EMPLOYEES WITH (NOLOCK)
   WHERE Id = @Id
END

--INSERT DATA
ALTER PROCEDURE [INSERT_EMPLOYEE] 
(
    @FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@DateofBirth DATE,
	@Email VARCHAR(50),
	@Salary FLOAT
)
AS
BEGIN

BEGIN TRY
    BEGIN TRAN
			   INSERT INTO Employees (FirstName,LastName,DateofBirth, Email, Salary) VALUES
			   (
					@FirstName,
					@LastName,
					@DateofBirth,
					@Email,
					@Salary
				)
    COMMIT TRAN
END TRY
BEGIN CATCH
    ROLLBACK TRAN
	END CATCH
END

--Update DATA
CREATE PROCEDURE [UPDATE_EMPLOYEE] 
(
    @Id        INT,
    @FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@DateofBirth DATE,
	@Email VARCHAR(50),
	@Salary FLOAT
)
AS
BEGIN
DECLARE @ROWCOUNT INT = 0
   BEGIN TRY 
        SET @ROWCOUNT=(SELECT COUNT(1)FROM Employees WITH (NOLOCK) WHERE Id=@Id)

		IF(@ROWCOUNT>0)
	BEGIN 
        BEGIN TRAN
			   UPDATE Employees SET
			   FirstName  =  @FirstName,
			   LastName  = @LastName,
			   DateofBirth= @DateofBirth,
			   Email = @Email,
			   Salary= @Salary
			  WHERE Id = @Id
    COMMIT TRAN
	END
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH	 
END

--Delete DATA
CREATE PROCEDURE [DELETE_EMPLOYEE] 
(
    @Id        INT
)
AS
BEGIN
DECLARE @ROWCOUNT INT = 0
   BEGIN TRY 
        SET @ROWCOUNT=(SELECT COUNT(1)FROM Employees WITH (NOLOCK) WHERE Id=@Id)

		IF(@ROWCOUNT>0)
	BEGIN 
        BEGIN TRAN
			   DELETE FROM Employees 
			   WHERE Id = @Id
    COMMIT TRAN
	END
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH	 
END