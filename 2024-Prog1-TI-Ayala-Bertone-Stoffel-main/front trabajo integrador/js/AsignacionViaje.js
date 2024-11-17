document.getElementById('Asignacion-Viaje').addEventListener('submit', function (event) {
    event.preventDefault();
    const fechaDesde = document.getElementById('fecha-desde').value;
    const fechaHasta = document.getElementById('fecha-hasta').value;
    const datos = {
        FechaEntregaDesde: fechaDesde,
        FechaEntregaHasta: fechaHasta
    };
    fetch('http://localhost:5247/Viaje', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(datos)
    })
    .then(response => response.json())
    .then(data => {
        console.log('Respuesta de la API:', data);
        alert(data.message);
    })
    .catch(error => {
        console.error('Error al enviar los datos:', error);
        alert(data.message);
    });

});

