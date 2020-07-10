using Microsoft.EntityFrameworkCore.Migrations;

namespace DapperDemo.Migrations
{
    public partial class storedProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROC usp_GetCompany
                    @CompanyId int
                AS 
                BEGIN 
                    SELECT *
                    FROM Companies
                    WHERE CompanyId = @CompanyId
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_GetALLCompany
                AS 
                BEGIN 
                    SELECT *
                    FROM Companies
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_AddCompany
                    @CompanyId int OUTPUT,
                    @Name varchar(MAX),
	                @Address  varchar(MAX),
	                @City varchar(MAX),
	                @State varchar(MAX),
	                @PostalCode varchar(MAX)
                AS
                BEGIN 
                    INSERT INTO Companies (Name, Address, City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode);
	                SELECT @CompanyId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_UpdateCompany
	                @CompanyId int,
                    @Name varchar(MAX),
	                @Address  varchar(MAX),
	                @City varchar(MAX),
	                @State varchar(MAX),
	                @PostalCode varchar(MAX)
                AS
                BEGIN 
                    UPDATE Companies  
	                SET 
		                Name = @Name, 
		                Address = @Address,
		                City=@City, 
		                State=@State, 
		                PostalCode=@PostalCode
	                WHERE CompanyId=@CompanyId;
	                SELECT @CompanyId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_RemoveCompany
                    @CompanyId int
                AS 
                BEGIN 
                    DELETE
                    FROM Companies
                    WHERE CompanyId  = @CompanyId
                END
                GO	
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
