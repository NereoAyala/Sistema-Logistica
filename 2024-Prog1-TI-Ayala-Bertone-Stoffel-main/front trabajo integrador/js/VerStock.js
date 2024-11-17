const formularioStock = document.getElementById('VerStock');
const tablaproductos = document.getElementById('TablaStock');
formularioStock.addEventListener('submit', function (event) {
    event.preventDefault();
    const limiteStock = document.getElementById('limiteStock').value;
    llenarTablaStock(limiteStock);
});
function llenarTablaStock(limiteStock) {
    fetch(`http://localhost:5247/Producto?limite=${limiteStock}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Error en la red');
            }
            return response.json();
        })
        .then(data => {
            tablaproductos.innerHTML = '';
            data.forEach(producto => {
                
                const fila = document.createElement('tr');
                fila.innerHTML = `
                    <td>${producto.nombre}</td>
                    <td>${producto.stockDisponible}</td>
                    <td>${producto.stockMinimo}</td>`;
                tablaproductos.appendChild(fila);
            });
        })
        .catch(error => {
            console.error('Error al obtener datos:', error);
        });
}
