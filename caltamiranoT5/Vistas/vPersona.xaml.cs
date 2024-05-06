using caltamiranoT5.Models;
using Microsoft.Win32;
using System.Diagnostics;

namespace caltamiranoT5.Vistas;

public partial class vPersona : ContentPage
{
	public vPersona()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.PersonRepo.AddNewPerson(txtPersona.Text);
        lblStatus.Text = App.PersonRepo.statusMessage;
        CargarPersonas();

    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        CargarPersonas();
        if (Listapersona.ItemsSource == null || ((List<Persona>)Listapersona.ItemsSource).Count == 0)
        {
            lblStatus.Text = "No hay personas registradas";
        }
    }
    private void CargarPersonas()
    {
        List<Persona> people = App.PersonRepo.GetAllPeople();
        Listapersona.ItemsSource = people;

        // Habilitar los botones de eliminar y actualizar solo si hay personas en la lista
        btnEliminar.IsEnabled = people.Count > 0;
        btnEditar.IsEnabled = people.Count > 0;
    }
    private async void btnEditar_Clicked(object sender, EventArgs e)
    {
        if (Listapersona.SelectedItem != null)
        {
            var selectedPerson = (Persona)Listapersona.SelectedItem;
            string newName = await DisplayPromptAsync("Modificar Persona", "Ingrese el nuevo nombre:", "Aceptar", "Cancelar", "", -1, Keyboard.Default, selectedPerson.Name);
            if (!string.IsNullOrEmpty(newName))
            {
                selectedPerson.Name = newName;
                App.PersonRepo.UpdatePerson(selectedPerson);
                lblStatus.Text = App.PersonRepo.statusMessage;
                CargarPersonas();
            }
        }
        else
        {
            lblStatus.Text = "Por favor seleccione una persona para modificar";
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (Listapersona.SelectedItem != null)
        {
            var selectedPerson = (Persona)Listapersona.SelectedItem;
            App.PersonRepo.DeletePerson(selectedPerson.id);
            lblStatus.Text = App.PersonRepo.statusMessage;
            CargarPersonas();
            btnEliminar.IsEnabled = false; // Desactivar el botón después de eliminar
        }
        else
        {
            lblStatus.Text = "Por favor seleccione una persona para eliminar";
        }
    }

    private void Listapersona_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Listapersona.SelectedItem != null)
        {
            btnEliminar.IsEnabled = true;
            btnEditar.IsEnabled = true;
        }
        else
        {
            btnEliminar.IsEnabled = false;
            btnEditar.IsEnabled = false;
        }
    }
}
