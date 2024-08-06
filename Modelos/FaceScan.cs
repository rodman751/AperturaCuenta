using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class FaceScan
    {
        public string FaceId { get; set; }  // Identificador único del rostro proporcionado por el Face API

        public string ImageUrl { get; set; }  // URL de la imagen del rostro

        public DateTime DetectedAt { get; set; }  // Fecha y hora de detección

        // Atributos faciales si el Face API proporciona estos datos
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Emotion { get; set; }

    }
}
