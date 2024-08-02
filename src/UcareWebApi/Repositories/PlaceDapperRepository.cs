namespace UcareApp.Repositories;

using UcareApp.Repositories.Base;
using UcareApp.Models;
using System.Data.SqlClient;
using Dapper;
using Npgsql;

public class PlaceDapperRepository : IPlaceRepository
{
    private readonly string connectionString = "Server=ucarepostgresqlsrv.postgres.database.azure.com;Database=postgres;Port=5432;User Id=ucare_admin;Password=Step_password;Ssl Mode=Require;";
    public async Task<IEnumerable<Place>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(this.connectionString);

        return await connection.QueryAsync<Place>("Select * from Places");
    }

    public async Task<Place> GetByIdAsync(Guid? id)
    {
        using var connection = new NpgsqlConnection(this.connectionString);

        return await connection.QueryFirstAsync<Place>($"Select * from Places Where Places.Id = '{id}'");
    }

    public async Task CreateAsync(Place? newPlace)
    {
        using var connection = new NpgsqlConnection(this.connectionString);

        await connection.ExecuteAsync($@"Insert into Places(Name, Adress, Longitude, Latitude) Values ('{newPlace.Name}', '{newPlace.Adress}', {newPlace.Longitude}, {newPlace.Latitude});");
    }

    public async Task DeleteAsync(Guid? id)
    {
        using var connection = new NpgsqlConnection(this.connectionString);

        var placesIds = await connection.QueryAsync<Guid?>("Select Id From Places");

        var containsId = placesIds.Contains(id.Value);

        if (containsId)
        {
            await connection.ExecuteAsync("DELETE FROM Places WHERE Places.Id = @Id", new { Id = id });
        }
    }

    public async Task<long> UpdateAsync(Place? place)
    {
        using var connection = new NpgsqlConnection(this.connectionString);

        return await connection.ExecuteAsync($@"Update Places
                                                Set name = @Name,
                                                    adress = @Adress,
                                                    longitude = @Longitude,
                                                    latitude = @Latitude,
                                                    workingDays = ARRAY[]::days[],
                                                    maintenanceid = null
                                                Where Places.id = @Id", new { Id = place.Id, 
                                                                                Name = place.Name, 
                                                                                Adress = place.Adress, 
                                                                                Longitude = place.Longitude, 
                                                                                Latitude = place.Latitude, });
    }
}
