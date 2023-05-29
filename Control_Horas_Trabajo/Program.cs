using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Horas_Trabajo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Desarrollador jose = new Desarrollador("Jose Nolasco", "jose_rivel@hotmail.com", "Nivel 1", new DateTime(2023, 05, 22));
            Desarrollador guillermo = new Desarrollador("Guillermo Eduardo", "eduardo@hotmail.com", "Nivel 1", new DateTime(2023, 05, 22));

            Proyecto capacitacion = new Proyecto("Training New Talent", "Nivel 1", 540, new DateTime(2023, 05, 22));
            Proyecto capacitacion2 = new Proyecto("Training New Talent 2", "Nivel 1", 540, new DateTime(2023, 05, 22));

            jose.AsignarProyecto(capacitacion);
            capacitacion.AsignarEmpleado(jose);

            guillermo.AsignarProyecto(capacitacion2);
            capacitacion2.AsignarEmpleado(guillermo);

            jose.CargarHorasTrabajadasProyecto(capacitacion, 9);
            jose.CargarHorasTrabajadasProyecto(capacitacion, 9);
            jose.CargarHorasTrabajadasProyecto(capacitacion, 9);
            jose.CargarHorasTrabajadasProyecto(capacitacion, 9);
            jose.CargarHorasTrabajadasProyecto(capacitacion, 9);
            Console.WriteLine("\n");
            guillermo.CargarHorasTrabajadasProyecto(capacitacion2, 9);
            guillermo.CargarHorasTrabajadasProyecto(capacitacion2, 9);
            guillermo.CargarHorasTrabajadasProyecto(capacitacion2, 9);
            guillermo.CargarHorasTrabajadasProyecto(capacitacion2, 8);
            guillermo.CargarHorasTrabajadasProyecto(capacitacion2, 9);

            Console.WriteLine("\nhoras trabajadas por " + jose.nombre + " en el proyecto " + capacitacion.nombre + ":" + jose.ObtenerHorasTrabajadas(capacitacion));
            Console.WriteLine("horas trabajadas por " + guillermo.nombre + " en el proyecto " + capacitacion2.nombre + ":" + capacitacion2.horasTrabajadas);
            Console.ReadLine();
        }

    }

    public class HorasTrabajadasProyecto
    {
        public empleado empleado;
        public Proyecto proyecto;
        public int horasTrabajadas = 0;

        public HorasTrabajadasProyecto(empleado empleado, Proyecto proyecto, int horas)
        {
            this.empleado = empleado;
            this.proyecto = proyecto;
            this.horasTrabajadas = horas;
        }
    }

    public class empleado
    {
        public string nombre;
        public string email;
        public string categoria;
        public DateTime fechaInicio;
        public List<Proyecto> proyectosAsignados;

        public empleado(string nombre, string email, string categoria, DateTime fechaInicio)
        {
            this.nombre = nombre;
            this.email = email;
            this.categoria = categoria;
            this.fechaInicio = fechaInicio;
            proyectosAsignados = new List<Proyecto>();
        }

        public void AsignarProyecto(Proyecto proyecto)
        {
            bool existe = false;

            for (int i = 0; i < proyecto.empleadosAsignados.Count; i++)
            {
                if (proyecto.empleadosAsignados[i].GetType().Name == "Desarrollador" && this.GetType().Name == "Desarrollador")
                {
                    Console.WriteLine("El " + proyecto.empleadosAsignados[i].GetType().Name + " " + this.nombre + " no puede ser asignado al proyecto " + proyecto.nombre + " porque ya tiene al desarrollador " + proyecto.empleadosAsignados[i].nombre);
                    existe = true;
                    break;
                }
            }

            if (!existe)
            {
                this.proyectosAsignados.Add(proyecto);
                Console.WriteLine("El proyecto " + proyecto.nombre + " fue asignado al " + this.GetType().Name + " " + this.nombre);
            }
        }

        public virtual void CargarHorasTrabajadasProyecto(Proyecto proyecto, int horas)
        {
            bool existe = false;

            for (int i = 0; i < this.proyectosAsignados.Count; i++)
            {
                if (this.proyectosAsignados[i] == proyecto)
                {
                    this.proyectosAsignados[i].horasTrabajadas += horas;
                    Console.WriteLine(this.nombre + " cargo " + horas + " horas al proyecto " + this.proyectosAsignados[i].nombre);
                    existe = true; break;
                }
            }

            if (!existe)
            {
                Console.WriteLine(this.nombre + " no puede cargar horas al proyecto " + proyecto.nombre + " porque no esta asignado ha este proyecto");
            }

        }

        public int ObtenerHorasTrabajadas(Proyecto proyecto)
        {

            for (int i = 0; i < this.proyectosAsignados.Count; i++)
            {
                if (this.proyectosAsignados[i] == proyecto)
                {
                    return this.proyectosAsignados[i].horasTrabajadas;
                }
            }

            return 0;
        }
    }

    public class Desarrollador : empleado
    {
        public Desarrollador(string nombre, string email, string categoria, DateTime fechaInicio) : base(nombre, email, categoria, fechaInicio)
        {
        }
    }

    public class Proyecto
    {
        public string nombre;
        public string categoria;
        public int duracionHoras;
        public DateTime fechaInicio;
        public int horasTrabajadas;
        public List<empleado> empleadosAsignados = new List<empleado>();

        public Proyecto(String nombre, string categoria, int horas, DateTime fechaInicio)
        {
            this.nombre = nombre;
            this.categoria = categoria;
            this.duracionHoras = horas;
            this.fechaInicio = fechaInicio;
            this.horasTrabajadas = 0;
        }
        public void AsignarEmpleado(empleado emp)
        {
            bool exite = false;

            for (int i = 0; i < this.empleadosAsignados.Count; i++)
            {
                if (this.empleadosAsignados[i].GetType().Name == "Desarrollador" && emp.GetType().Name == "Desarrollador")
                {
                    Console.WriteLine("El desarrollador " + emp.nombre + " no pudo ser asignado al proyecto " + this.nombre + ", porque ya contiene al desarrollador " + this.empleadosAsignados[i].nombre);
                    exite = true; break;
                }
            }

            if (!exite)
            {
                this.empleadosAsignados.Add(emp);
                Console.WriteLine("El " + emp.GetType().Name + " " + emp.nombre + " ha sido asignado al proyecto " + this.nombre + "\n");
            }
        }

    }
}
