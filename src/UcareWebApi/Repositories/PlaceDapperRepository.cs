namespace UcareApp.Repositories;

using UcareApp.Repositories.Base;
using UcareApp.Models;
using Dapper;
using Npgsql;

public class PlaceDapperRepository : IPlaceRepository
{
    private readonly string? connectionString;
    public PlaceDapperRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("PlacesDb");
    }

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

        await connection.ExecuteAsync($"Insert into Places(Name, Adress, Longitude, Latitude, ServiceType, WorkingDays, PhotoUrl, Rating) Values (@Name, @Adress, @Longitude, @Latitude, @ServiceType, @WorkingDays, @PhotoUrl, @Rating);",
                                        new { Name = newPlace?.Name,
                                              Adress = newPlace?.Adress,
                                              Longitude = newPlace?.Longitude,
                                              Latitude = newPlace?.Latitude,
                                              ServiceType = newPlace?.ServiceType,
                                              WorkingDays = newPlace?.WorkingDays,
                                              PhotoUrl = newPlace?.PhotoUrl,
                                              Rating = newPlace?.Rating,
                                            });
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
                                                    workingdays = @WorkingDays,
                                                    servicetype = @ServiceType,
                                                    photourl = @PhotoUrl,
                                                    rating = @Rating
                                                Where Places.id = @Id", new { Id = place?.Id, 
                                                                                Name = place?.Name, 
                                                                                Adress = place?.Adress, 
                                                                                Longitude = place?.Longitude, 
                                                                                Latitude = place?.Latitude,
                                                                                WorkingDays = place?.WorkingDays,
                                                                                ServiceType = place?.ServiceType,
                                                                                PhotoUrl = place?.PhotoUrl,
                                                                                Rating = place?.Rating
                                                                                 });
    }
}
