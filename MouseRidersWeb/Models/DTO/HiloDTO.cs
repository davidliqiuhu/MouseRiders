﻿using MouseRidersGenNHibernate.Enumerated.MouseRiders;
using MouseRidersWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Definición clase HiloDTO
namespace MouseRidersWeb.DTO
{
    public class HiloDTO
    {
        //Atributo Id
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        //Nuevo atributo
        //ID_Creador
        public int ID_Creador { get; set; }

        //Atributo Titulo
        [Display(Prompt = "Titulo", Description = "Titulo del articulo", Name = "Título del hilo:")]
        [Required(ErrorMessage = "Debe tener un titulo")]
        [StringLength(maximumLength: Globals.TITULO_MAX_LENGTH, ErrorMessage = "El Titulo no puede tener más de {0} caracteres")]
        public string Titulo { get; set; }
        

        //Atributo fecha
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Prompt = "Fecha", Description = "Fecha del comentario", Name = "Fecha:")]
        [Required(ErrorMessage = "Debe de indicar una fecha para el comentario")]
        public Nullable<DateTime> Fecha { get; set; }


        //Atributo Creador
        [Display(Prompt = "Creador", Description = "Creador del hilo", Name = "Creador:")]
        [Required(ErrorMessage = "Debe tener un creador")]
        [StringLength(maximumLength: Globals.CREADOR_MAX_LENGTH, ErrorMessage = "El nombre del creador no puede tener más de {0} caracteres")]
        public string Creador { get; set; }

        //Atributo NumComentarios
        [Display(Prompt = "Numero de comentarios", Description = "Numero de comentarios del hilo", Name = "Numero de comentarios:")]
        //[StringLength(maximumLength: Globals.CONTENIDO_MAX_LENGTH, ErrorMessage = "El contenido descargable no puede tener más de {0} caracteres")]
        public int NumComentarios { get; set; }

        //Atributo Comentario
        [ScaffoldColumn(false)]
        [Display(Prompt = "Comentarios", Description = "Comentarios", Name = "Comentarios")]
        public IList<ComentarioDTO> Comentario { get; set; }

        //Atributo Primer Comentario
        [Display(Prompt = "Primer Comentario", Description = "Primer comentario del hilo", Name = "Texto del primer comentario del hilo:")]
        [Required(ErrorMessage = "Debe tener un primer comentario")]
        [StringLength(maximumLength: Globals.TITULO_MAX_LENGTH, ErrorMessage = "El primer comentario no puede tener más de {0} caracteres")]
        public string PrimerComentario { get; set; }

    }
}