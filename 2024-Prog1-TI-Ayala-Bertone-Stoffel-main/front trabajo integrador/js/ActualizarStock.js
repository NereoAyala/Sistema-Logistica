document.getElementById('formulario-actualizar-stock').addEventListener('submit', function (event) {
    event.preventDefault();
    const id = document.getElementById('id').value;
    const stock = document.getElementById('stock').value;
    console.log('Datos obtenidos:', id, stock);
    fetch(`http://localhost:5247/Producto/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(stock)
    })

        /*.then(response => response.json())
        .then(data => {
            console.log('Respuesta de la API:', data);
            alert(data.message);
        })
        .catch(error => {
            console.error('Error al enviar los datos:', error);
            alert('Hubo un error al procesar la actualización del stock');
        });*/
        .then(response => response.json())
        .then(data => {
            console.log('Respuesta de la API:', data);
            if (data.success) {
                alert('Se ha actualizado el stock');
            } else {
                alert('No se ha podido actualizar el stock');
            }
        })
        .catch(error => {
            console.error('Error al enviar los datos:', error);
            alert('Hubo un error al procesar la actualización del stock');
        });
       
       
});

