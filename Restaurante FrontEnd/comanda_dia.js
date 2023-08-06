let fechaActual = new Date();

let dia = fechaActual.getDate();
let mes = fechaActual.getMonth() + 1;
let anio = fechaActual.getFullYear();

getComandaDia(dia, mes, anio);

function getComandaDia(dia, mes, anio) {
  fechaString = anio + '/' + mes + '/' + dia;
  let comandas = [];

  fetch(`https://localhost:7246/api/v1/Comanda?fecha=${fechaString}`)
    .then(res => res.json())
    .then(response => {
      comandas = response;

      let registroComanda = document.getElementById("comanda");
      registroComanda.innerHTML = "";

      if (comandas.length === 0) {
        mostrarVacio();
      } else {
        ocultarVacio();

        comandas.forEach(comanda => {
          let comandaDiv = document.createElement("div");
          let idComanda = document.createElement("p");
          let precioTotalComanda = document.createElement("p");
          let fechaComanda = document.createElement("p");
          let formaEntregaComanda = document.createElement("p");

          idComanda.innerHTML = "<strong>Id: </strong> " + comanda.id;
          precioTotalComanda.innerHTML = `<strong>Precio Total:</strong><strong>$${comanda.total}</strong>`;
          fechaComanda.innerHTML = "<strong>Fecha: </strong> " + comanda.fecha;
          formaEntregaComanda.innerHTML = "<strong>Forma de Entrega: </strong> " + comanda.formaEntrega.descripcion;

          comandaDiv.classList.add("comanda_detalle");
          precioTotalComanda.classList.add("datos_comanda");
          fechaComanda.classList.add("datos_comanda");
          formaEntregaComanda.classList.add("datos_comanda");

          comandaDiv.appendChild(idComanda);
          comanda.mercaderias.forEach(mercaderia => {
            let mercaderiaDiv = document.createElement("div");
            let nombre = document.createElement("h2");
            let precio = document.createElement("p");

            nombre.innerText = mercaderia.nombre;
            precio.innerHTML = "<strong>$</strong> " + mercaderia.precio;

            mercaderiaDiv.appendChild(nombre);
            mercaderiaDiv.appendChild(precio);

            mercaderiaDiv.classList.add("mercaderia_comanda");
            comandaDiv.appendChild(mercaderiaDiv);
          });
          comandaDiv.appendChild(formaEntregaComanda);
          comandaDiv.appendChild(precioTotalComanda);
          comandaDiv.appendChild(fechaComanda);
          registroComanda.appendChild(comandaDiv);

        });
      }
    });
}

function ocultarVacio(){
  let comandaVacíaDiv = document.getElementById("comanda_vacia");
  comandaVacíaDiv.style.display = "none";
}

function mostrarVacio(){
  let comandaVacíaDiv = document.getElementById("comanda_vacia");
  comandaVacíaDiv.style.display = "block";
}





