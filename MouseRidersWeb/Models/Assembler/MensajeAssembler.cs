﻿using MouseRidersGenNHibernate.CAD.MouseRiders;
using MouseRidersGenNHibernate.CEN.MouseRiders;
using MouseRidersWeb.DTO;
using MouseRidersGenNHibernate.EN.MouseRiders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseRidersWeb.Assembler
{
    public class MensajeAssembler
    {
        public MensajeDTO Convert(MensajeEN men)
        {
            MensajeDTO menDTO = null;
            if (men != null)
            {
                menDTO = new MensajeDTO();
                menDTO.Id = men.Id;
                menDTO.Asunto = men.Asunto;
                menDTO.Texto = men.Texto;
                menDTO.Tipo = men.Tipo;
                menDTO.Adjunto = men.Adjunto;
            }
            return menDTO;
        }

    }
}
