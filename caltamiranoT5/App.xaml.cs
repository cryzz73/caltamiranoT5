using caltamiranoT5.Vistas;
using System.Security.Cryptography.X509Certificates;

namespace caltamiranoT5
{
    public partial class App : Application
    {
          public static PersonRepository PersonRepo { get; set; }
            public App( PersonRepository personRepository)
             {
            InitializeComponent();

            MainPage = new Vistas.vPersona();
           
            PersonRepo = personRepository;
        }
         
        }
    }
