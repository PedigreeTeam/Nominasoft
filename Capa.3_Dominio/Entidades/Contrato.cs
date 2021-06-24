﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa._3_Dominio.Entidades
{
    public class Contrato
    {
        private int id_contrato;
        private bool asignacionFamiliar;
        private string cargo;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private int horasContratadasPorSemana;
        private int valorHora;
        private bool estado;

        private Afp afp;
        private Empleado empleado;
        private Periodo periodo;
        private ConceptoIngresoDeDescuento conceptoIngresoDeDescuento;


        public int Id_contrato { get => id_contrato; set => id_contrato = value; }
        public bool AsignacionFamiliar { get => asignacionFamiliar; set => asignacionFamiliar = value; }
        public string Cargo { get => cargo; set => cargo = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public int HorasContratadasPorSemana { get => horasContratadasPorSemana; set => horasContratadasPorSemana = value; }
        public int ValorHora { get => valorHora; set => valorHora = value; }
        public bool Estado { get => estado; set => estado = value; }

        public Afp Afp { get => afp; set => afp = value; }
        public Empleado Empleado { get => empleado; set => empleado = value; }
        public Periodo Periodo { get => periodo; set => periodo = value; }
        public ConceptoIngresoDeDescuento ConceptoIngresoDeDescuento { get => conceptoIngresoDeDescuento; set => conceptoIngresoDeDescuento = value; }


        /// <summary>
        /// La fecha fin de un contrato debe ser superior a la fecha 
        /// de inicio con una diferencia de tres meses como mínimo
        /// y 12 meses como máximo
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidarFechaFinContrato()
        {
            if ((FechaFin - FechaInicio).TotalDays > 90 && (FechaFin - FechaInicio).TotalDays < 365)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// La fecha de inicio de un contrato debe ser superior a la  fecha fin de su anterior contrato si lo tuviera.
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidarFechaInicioContrato()
        {
            Contrato anterior = null;
            try
            {
                anterior = Empleado.Contratos[0];
            }
            catch (Exception e)
            {
                anterior = null;
            }
            if (anterior != null && Id_contrato == anterior.id_contrato) //al editar
            {
                return true;
            }
            if (anterior == null || FechaInicio > anterior.FechaFin || !anterior.estado)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///  El total de horas contratadas por semana sólo puede 
        ///  ser de 8 a 40 horas, pero sólo valores enteros con 
        ///  diferencia de 4 horas a partir de las 8 horas. Por 
        ///  ejemplo: 8, 12, 16, … , 36, 40
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidarHoras()
        {
            if (8 <= HorasContratadasPorSemana && HorasContratadasPorSemana <= 40 && HorasContratadasPorSemana % 4 == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  El valor hora de un contrato sólo puede ser desde 10 
        ///  soles a 60 soles, pero sólo valores entero
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidarValorHoras()
        {
            if (10 <= ValorHora && ValorHora <= 60 && ValorHora - (int)ValorHora == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///   Un contrato está vigente si su fecha fin es mayor o igual 
        ///  a la fecha actual y no está anulado.
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidarVigenciaContrato()
        {
            DateTime today = DateTime.Now;
            
            //Console.WriteLine(DateTime.Compare(today, FechaFin));
            if (today <= FechaFin && Estado == true)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Los contratos que pueden ser procesados en un
        /// periodo activo son los que tienen fecha fin mayor a la
        /// fecha de inicio del periodo y además no están anulados.
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidarContratoProcesar()
        {


                if (FechaFin > Periodo.FechaInicio && Estado)
                {
                    return true;
                }
            
            return false;
        }
    }
}
