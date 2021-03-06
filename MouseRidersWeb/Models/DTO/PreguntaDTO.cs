﻿using MouseRidersGenNHibernate.Enumerated.MouseRiders;
using MouseRidersWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Definición clase PreguntaDTO
namespace MouseRidersWeb.DTO
{
    public class PreguntaDTO
    {
        //Atributo id
        [ScaffoldColumn(false)]
        public int id { get; set; }

        //Atributo pregunta
        [Display(Prompt = "Pregunta", Description = "Cuerpo de la pregunta", Name = "PreguntaN ")]
        //[Required(ErrorMessage = "Debe indicar el texto de la pregunta")] // Con el nuevo formulario de encuesta es mejor asi
        [StringLength(maximumLength: 200, ErrorMessage = "La pregunta no puede tener más de 200 caracteres")]
        public string Pregunta { get; set; }

        //Atributo tipo
        public MouseRidersGenNHibernate.Enumerated.MouseRiders.T_PreguntaEnum Tipo { get; set; }

        //Atributo pertenece
        [Display(Prompt = "Encuesta", Description = "Encuesta a la que pertenece la pregunta", Name = "EncuestaN ")]
        [Required(ErrorMessage = "Debe indicar la encuesta en la que aparece esta pregunta")]
        [DataType(DataType.Currency, ErrorMessage = "El dato introducido debe de ser una encuesta")]
        public MouseRidersWeb.DTO.EncuestaDTO Pertenece { get; set; }


        //Atributo tiene
        [Display(Prompt = "Respuestas", Description = "Respuestas de la pregunta", Name = "RespuestasN ")]
        [Required(ErrorMessage = "Debe indicar las respuestas posibles de la pregunta")]
        [DataType(DataType.Currency, ErrorMessage = "El dato introducido debe de ser una lista de respuestas")]
        public System.Collections.Generic.IList<MouseRidersGenNHibernate.EN.MouseRiders.RespuestaEN> Tiene { get; set; }

    }
}
