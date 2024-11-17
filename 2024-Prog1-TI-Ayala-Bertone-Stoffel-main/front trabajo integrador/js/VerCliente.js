function llenarTablaclientes() {
    fetch(`http://localhost:5247/Cliente`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Error en la red');
            }
            return response.json();
        })
        .then(data => {
            const tablaclientes = document.getElementById('tabla-clientes');
            tablaclientes.innerHTML = '';

            data.forEach(cliente => {
                const fecha_nacimiento = new Date(cliente.fechaNacimiento).toLocaleDateString();
                const fila = document.createElement('tr');
                fila.innerHTML = `
                    <td>${cliente.dniCliente}</td>
                    <td>${cliente.nombre}</td>
                    <td>${cliente.apellido}</td>
                    <td>${cliente.email}</td>
                    <td>${cliente.telefono}</td>
                    <td>${cliente.latitud}</td>
                    <td>${cliente.longitud}</td>
                    <td>${fecha_nacimiento}</td>`;
                tablaclientes.appendChild(fila);
            });
        })
        .catch(error => {
            console.error('Error al obtener datos:', error);
        });
}
document.addEventListener("DOMContentLoaded", llenarTablaclientes);
