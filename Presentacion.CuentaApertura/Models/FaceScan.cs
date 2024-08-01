namespace Presentacion.CuentaApertura.Models
{
    public class FaceScan
    {
        //public int Id { get; set; }  // Identificador único para la base de datos

        public string FaceId { get; set; }  // Identificador único del rostro proporcionado por el Face API

        public string ImageUrl { get; set; }  // URL de la imagen del rostro

        public DateTime DetectedAt { get; set; }  // Fecha y hora de detección

        // Atributos faciales si el Face API proporciona estos datos
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Emotion { get; set; }

    }
}
