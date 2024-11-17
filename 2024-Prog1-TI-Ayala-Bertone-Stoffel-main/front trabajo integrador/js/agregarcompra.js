document.getElementById('compraForm').addEventListener('submit', function(event) {
    event.preventDefault();
    const id = document.getElementById('id').value;
    const cantidad = document.getElementById('cantidad').value;
    const dni = document.getElementById('dni').value;
    const fecha = document.getElementById('fecha_entrega').value;
    console.log('Datos obtenidos:', id, cantidad, fecha);
    const datos = {
        CodProducto: id,
        CantidadComprado: cantidad,
        DniCliente: dni,
        FechaEntrega: fecha
    };
    fetch('http://localhost:5247/Compra', {
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



