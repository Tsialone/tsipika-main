using System;
using System.Data;
using Npgsql;  // Pour PostgreSQL

public class MyConnection
{
    public IDbConnection connection;

    // Le constructeur prend l'URL de connexion comme paramètre
    public MyConnection(string connectionString = "Host=localhost;Username=postgres;Password=bloodseeker;Database=tennis")
    {
        try
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("La chaîne de connexion ne peut pas être vide.");
            }

            connection = new NpgsqlConnection(connectionString);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // Méthode pour ouvrir la connexion
    public void OpenConnection()
    {
        try
        {
            connection.Open();
            //Console.WriteLine("Connexion réussie !");
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Erreur lors de l'ouverture de la connexion : {ex.Message}");
            // Ajout de la pile d'appels pour un meilleur débogage
            //Console.WriteLine(ex.StackTrace);
        }
    }

    // Méthode pour fermer la connexion
    public void CloseConnection()
    {
        try
        {
            connection.Close();
            //Console.WriteLine("Connexion fermée.");
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Erreur lors de la fermeture de la connexion : {ex.Message}");
            //Console.WriteLine(ex.StackTrace);
        }
    }

    // Méthode pour exécuter une commande SQL de type SELECT et afficher les résultats
    public void ExecuteQueryWithResult(string query)
    {
        try
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;
                using (var reader = cmd.ExecuteReader())
                {
                    // Parcours des résultats (ResultSet)
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            //Console.Write(reader.GetValue(i) + "\t");
                        }
                        //Console.WriteLine();
                    }
                }
                //Console.WriteLine("Commande exécutée avec succès.");
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Erreur lors de l'exécution de la requête : {ex.Message}");
            //Console.WriteLine(ex.StackTrace);  // Ajout de la pile d'appels pour le débogage
        }
        finally
        {
            CloseConnection();  // Assurez-vous que la connexion est fermée même si une erreur se produit
        }
    }

    // Méthode pour exécuter une commande SQL de type non SELECT (par exemple, INSERT, UPDATE, DELETE)
    public void ExecuteNonQuery(string query)
    {
        try
        {
            OpenConnection();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                // //Console.WriteLine("Commande exécutée avec succès.");
            }
        }
        catch (Exception ex)
        {
            // //Console.WriteLine($"Erreur lors de l'exécution de la requête : {ex.Message}");
            // //Console.WriteLine(ex.StackTrace);  // Ajout de la pile d'appels pour le débogage
        }
        finally
        {
            CloseConnection();  // Assurez-vous que la connexion est fermée même si une erreur se produit
        }
    }
    public void ExecuteNonQueryWithParams(string query, object parameters)
{
    try
    {
        OpenConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = query;

            // Ajout des paramètres dynamiquement
            foreach (var property in parameters.GetType().GetProperties())
            {
                var name = property.Name;
                var value = property.GetValue(parameters);
                cmd.Parameters.Add(new NpgsqlParameter($"@{name}", value ?? DBNull.Value));
            }

            cmd.ExecuteNonQuery();
            Console.WriteLine("Commande exécutée avec succès.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'exécution de la requête : {ex.Message}");
    }
    finally
    {
        CloseConnection();
    }
}
}
