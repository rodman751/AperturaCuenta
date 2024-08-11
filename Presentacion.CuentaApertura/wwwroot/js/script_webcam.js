const video = document.getElementById('inputVideo');
const canvas = document.getElementById('overlay');

(async () => {
    const stream = await navigator.mediaDevices.getUserMedia({ video: {} });
    video.srcObject = stream;
})();

function captureImage() {
    const canvas = document.createElement('canvas');
    const context = canvas.getContext('2d');

    // Configura el tamaño del canvas para que coincida con el tamaño del video
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;

    // Dibuja el fotograma actual del video en el canvas
    context.drawImage(video, 0, 0, canvas.width, canvas.height);

    // Convierte el contenido del canvas a una imagen base64
    const base64Image = canvas.toDataURL('');




    return base64Image;
}

async function onPlay() {
    const MODEL_URL = '/lib/weights/';

    await faceapi.loadSsdMobilenetv1Model(MODEL_URL)
    await faceapi.loadFaceLandmarkModel(MODEL_URL)
    await faceapi.loadFaceRecognitionModel(MODEL_URL)
    await faceapi.loadFaceExpressionModel(MODEL_URL)

    let fullFaceDescriptions = await faceapi.detectAllFaces(video)
        .withFaceLandmarks()
        .withFaceDescriptors()
        .withFaceExpressions();

    //const dims = faceapi.matchDimensions(canvas, video, true);
    //const resizedResults = faceapi.resizeResults(fullFaceDescriptions, dims);

    //faceapi.draw.drawDetections(canvas, resizedResults);
    //faceapi.draw.drawFaceLandmarks(canvas, resizedResults);
    //faceapi.draw.drawFaceExpressions(canvas, resizedResults, 0.05);

    if (fullFaceDescriptions.length > 0 && fullFaceDescriptions[0].expressions.happy > 0.5) {
        const base64Image = captureImage();
       
        await sendImageToServer(base64Image);  // Enviar la imagen al servidor
       //console.log("", base64Image)
       
    }


    setTimeout(() => onPlay(), 100)



}

async function sendImageToServer(base64Image) {
    try {
        
        const response = await fetch('/Steps/Camara2', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(base64Image)
        });

        if (response.ok) {
            console.log('Imagen enviada con éxito');
            const btn = document.getElementById('btnContinuar');
            console.log(btn);
            if (btn) {
                btn.style.display = 'block';
            } else {
                console.error('Botón no encontrado');
            }
        } else {
            console.error('Error al enviar la imagen', response.status, await response.text());
        }
    } catch (error) {
        console.error('Error en la solicitud de red:', error);
    }
}

