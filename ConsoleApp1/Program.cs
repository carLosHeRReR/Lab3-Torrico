using System.Data.SqlClient;
using System.Data;


class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-23\\SQLEXPRESS;Initial Catalog=TECSUP2023;User ID=userTecsup;Password=123456";


    static void Main()
    {
        var list = ListarTrabajadoresListaObjetos();
        foreach (var item in list)
        {
            Console.WriteLine(item.IdTrabajador + "\t" + item.Nombre + "\t" + item.Apellido + "\t" + item.Sueldo + "\t" + item.FechaNacimiento);
        }

    }

    //De forma desconectada
    private static DataTable ListarTrabajadoresDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Trabajdores";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }

    //De forma conectada
    private static List<Trabajador> ListarTrabajadoresListaObjetos()
    {
        List<Trabajador> trabajador = new List<Trabajador>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT IdTrabajador,Nombre,Apellido,Sueldo,FechaNacimiento FROM Trabajdores";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            trabajador.Add(new Trabajador
                            {
                                IdTrabajador = (int)reader["IdTrabajador"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Sueldo = (decimal)reader["Sueldo"],
                                FechaNacimiento = (DateTime)reader["FechaNacimiento"]
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return trabajador;

    }


}
