using UnityEngine;
using System;
using System.Collections.Generic;

namespace UnityUtils
{
    public class Tiempo
    {
        #region Atributos
        public float alarma;

        bool cronometroIniciado   = false;
        bool temporizadorIniciado = false;

        float tiempoCronometro;
        float tiempoTemporizador = float.MaxValue;
        #endregion

        #region Propiedades
        public float TiempoCronometro { get => tiempoCronometro; }
        public float TiempoTemporizador { get => tiempoTemporizador; }
        public bool EstaTemporizadorAcabado { get => tiempoTemporizador <= 0; }
        public bool SeHaActivadoAlarma { get => tiempoCronometro >= alarma; }
        #endregion

        #region M�todos
        public float Cronometrar()
        {
            if (!cronometroIniciado)
            {
                cronometroIniciado = true;
                return tiempoCronometro = 0;
            }
            return tiempoCronometro += Time.deltaTime;
        }

        public float Temporizar(float segundos, bool tiempoNegativo = false)
        {
            if (!temporizadorIniciado)
            {
                temporizadorIniciado = true;
                return tiempoTemporizador = segundos;
            }
            if (!tiempoNegativo && tiempoTemporizador <= 0) return 0;

            return tiempoTemporizador -= Time.deltaTime;
        }

        public void ResetearCronometro() => cronometroIniciado = false;

        public void ResetearTemporizador() => temporizadorIniciado = false;
        #endregion
    }

    public class Tareas
    {
        class Tarea
        {
            public float segundosHastaEjecutarse;
            public Action accion;
        }

        #region Atributos

        /// <summary>
        /// Es el tiempo que se ha estado ejecutando Update(),
        /// si se deja de llamar a este m�todo el tiempo en el que se ejecutar�n
        /// las tareas restantes al volver a llamar a este m�todo seguir� siendo el mismo que
        /// que cuando se dejo de llamar.
        /// </summary>
        float tiempoEjecutado;

        private readonly List<Tarea> lista = new List<Tarea>();

        #endregion

        #region M�todos

        /// <summary>
        /// A�ade una nueva tarea que se ejecutar� transcurridos los segundos indicados.
        /// </summary>
        /// <param name="segundos"></param>
        /// <param name="accion"></param>
        public void Aniadir(float segundos, Action accion)
        {
            var tarea = new Tarea
            {
                segundosHastaEjecutarse = Time.time + segundos,
                accion = accion
            };
            lista.Add(tarea);
        }

        /// <summary>
        /// A�ade una nueva tarea que se ejecutar� despu�s de la anterior
        /// en el n�mero indicado de segundos.
        /// </summary>
        /// <param name="segundos"></param>
        /// <param name="accion"></param>
        public void AniadirRelativo(float segundos, Action accion)
        {
            // En caso de que no haya tareas se llamar� a Aniadir()
            // ya que no puedes a�adir de forma relativa si no hay ninguna tarea
            if (lista.Count == 0)
            {
                Aniadir(segundos, accion);
                return;
            }

            var ultimaTarea = lista[lista.Count - 1];

            lista.Add(new Tarea
            {
                segundosHastaEjecutarse = ultimaTarea.segundosHastaEjecutarse + segundos,
                accion = accion
            });
        }

        /// <summary>
        /// Se coloca en un Update para poder ir ejecutando cada tarea una detr�s de otra.
        /// </summary>
        /// <param name="tiempoRelativo">
        /// Si es true entonces cuando se deje de llamar a este m�todo el tiempo
        /// de las tareas restantes para que se llegasen a ejecutar seguir� siendo
        /// el mismo cuando se vuelva a llamar.
        /// </param>
        public void Update(bool tiempoRelativo = false)
        {
            tiempoEjecutado += Time.deltaTime;

            var tiempo = Time.time;
            if (tiempoRelativo) tiempo = tiempoEjecutado;

            foreach (var tarea in lista)
                if (tiempo > tarea.segundosHastaEjecutarse)
                {
                    tarea.accion();
                    lista.Remove(tarea);
                    break;
                }
        }

        #endregion
    }
}