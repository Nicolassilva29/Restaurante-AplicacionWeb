mostrarImagenFondo();
ocultarPedidoRealizado();
GetAllMercaderia();

//GET TODAS LAS MERCADERIAS

function GetAllMercaderia(){
  let mercaderias = []
  fetch('https://localhost:7246/api/v1/Mercaderia')
    .then(res => res.json())
    .then(response => {
      mercaderias = response

      let carta = document.getElementById("carta")
      if (mercaderias.length === 0) {
        return;
      }
      else {
        carta.innerHTML = "";
      }
      mercaderias.forEach(mercaderia => {

        let mercaderiaDiv = document.createElement("div");
        let imagen = document.createElement("img");
        let nombre = document.createElement("h2");
        let precio = document.createElement("p");
        let botonDetalles = document.createElement("button");
        let botonPedir = document.createElement("button");

        nombre.innerText = mercaderia.nombre;
        precio.innerHTML = "<strong>Precio: $</strong> " + mercaderia.precio;
        imagen.setAttribute("src", mercaderia.imagen);
        botonDetalles.innerText = "Detalles";
        botonPedir.innerText = "Pedir";


        botonDetalles.classList.add("boton_detalles");
        botonPedir.classList.add("boton_pedir");        

        botonDetalles.addEventListener("click", () => {
          mostrarDetalle(mercaderia.id, mercaderiaDiv);
        });

        botonPedir.addEventListener("click", () => {
          listarMercaderia(mercaderia.nombre, mercaderia.precio, mercaderia.imagen, mercaderia.id);
        });

        mercaderiaDiv.appendChild(nombre);
        mercaderiaDiv.appendChild(precio);
        mercaderiaDiv.appendChild(botonDetalles);
        mercaderiaDiv.appendChild(botonPedir);
        mercaderiaDiv.appendChild(imagen);


        carta.appendChild(mercaderiaDiv);
      });

    })
}

//BOTON CREAR COMANDA

let botonComanda = document.getElementById("crear_comanda");
botonComanda.addEventListener("click", () => {

  const allDiv = document.getElementById("lista_comanda").getElementsByTagName("div");
  let mercaderiaList = Array.from(allDiv).flatMap(div => {
  const id = div.id;
  const textoDiv = div.querySelector(".texto_info_pedido");
  const cantidadSpan = textoDiv ? textoDiv.querySelector("span") : null;
  const cantidad = cantidadSpan ? parseInt(cantidadSpan.innerText) : 0;
  return Array(cantidad).fill(id);
});

  console.log(mercaderiaList);

  if (mercaderiaList.length > 0) {
    if (forma !== 1 && forma !== 2 && forma !== 3) {
      alert("Por favor, elige una forma de entrega.");
    }
    else {
      CrearComanda(mercaderiaList, forma);
      document.getElementById("lista_comanda").innerHTML = "";
      actualizarPrecioTotal();
      formaEntregaButtons.forEach(btn => btn.classList.remove("selected"));
      forma="";
      pedidoRealizado();
    }
  } else {
    alert("Elige una mercadería para poder crear la comanda");
  }
});

//LISTAR MERCADERIA EN EL PEDIDO

function listarMercaderia(nombre, precio, imagen, id) {
  let pedido = document.getElementById("lista_comanda");
  let detalleDiv;
  let mercaderiaExistente = Array.from(pedido.children).find(mercaderiaDiv => {
    let nombreElement = mercaderiaDiv.querySelector("h2");
    return nombreElement.innerText === nombre;
  });

  let botonQuitar;

  if (mercaderiaExistente) {
    let cantidadElement = mercaderiaExistente.querySelector("span");
    let cantidad = parseInt(cantidadElement.innerText);
    cantidad++;
    cantidadElement.innerText = cantidad;
    cantidadElement.classList.add("animate__animated", "animate__heartBeat"); 
    cantidadElement.addEventListener("animationend", () => {
      cantidadElement.classList.remove("animate__animated", "animate__heartBeat"); 
    });
    botonQuitar = mercaderiaExistente.querySelector("button");
    detalleDiv = mercaderiaExistente;
    actualizarPrecioTotal();

    let detalleDivRect = detalleDiv.getBoundingClientRect();
    let pedidoRect = pedido.getBoundingClientRect();

    let offsetTop = detalleDivRect.top - pedidoRect.top + pedido.scrollTop;
    let offsetBottom = offsetTop + detalleDivRect.height;

    let visibleAreaTop = pedido.scrollTop;
    let visibleAreaBottom = visibleAreaTop + pedido.clientHeight;

    if (offsetTop < visibleAreaTop || offsetBottom > visibleAreaBottom) {
      pedido.classList.add("scroll-transition"); // Agregar clase CSS para la transición suave

      if (offsetTop < visibleAreaTop) {
        pedido.scrollTop = offsetTop;
      } else {
        pedido.scrollTop = offsetBottom - pedido.clientHeight;
      }
    }
  } else {
    detalleDiv = document.createElement("div");
    let nombreElement = document.createElement("h2");
    let precioElement = document.createElement("p");
    //let imagenElement = document.createElement("img");
    let cantidadElement = document.createElement("span");
    let textoDiv = document.createElement("div");
    let textoinf = document.createElement("div");
    detalleDiv.setAttribute("id", id);

    botonQuitar = document.createElement("button");

    nombreElement.innerText = nombre;
    precioElement.innerHTML = `<strong>$</strong> ${precio}`;
    //imagenElement.setAttribute("src", imagen);
    botonQuitar.innerText = "Quitar";
    cantidadElement.innerText = "1";

    textoDiv.appendChild(nombreElement);
    textoDiv.appendChild(cantidadElement);

    textoinf.appendChild(precioElement);
    textoinf.appendChild(botonQuitar);
    detalleDiv.appendChild(textoDiv);
    detalleDiv.appendChild(textoinf);


    textoDiv.classList.add("texto_info_pedido");
    textoinf.classList.add("textoinf_info_pedido");

    detalleDiv.classList.add("info_pedido");
    pedido.appendChild(detalleDiv);

    pedido.classList.add("scroll-transition");
    pedido.scrollTop = pedido.scrollHeight;
  }

  actualizarPrecioTotal();
  ocultarImagenFondo();
  ocultarPedidoRealizado();

  setTimeout(() => {
    pedido.classList.remove("scroll-transition");
  }, 500);
  
}

//BOTON QUITAR

document.addEventListener("click", function(event) {
  let botonQuitar = event.target;
  if (botonQuitar.tagName === "BUTTON" && botonQuitar.innerText === "Quitar") {
    let detalleDiv = botonQuitar.parentElement.parentElement;
    let cantidadElement = detalleDiv.querySelector("span");

    if (cantidadElement) {
      let cantidad = parseInt(cantidadElement.innerText);
      if (cantidad > 1) {
        cantidad--;
        cantidadElement.innerText = cantidad;
      } else {
        detalleDiv.remove();
      }
      actualizarPrecioTotal();
      let pedido = document.getElementById("lista_comanda");
      if (pedido.childElementCount === 0) {
        forma = "";
        formaEntregaButtons.forEach(btn => btn.classList.remove("selected"));
        mostrarImagenFondo();
      }
    }
  }
});

//MOSTRAR EL DETALLE DE LA MERCADERIA

function mostrarDetalle(mercaderiaId, mercaderiaDiv) {
  fetch(`https://localhost:7246/api/v1/Mercaderia/${mercaderiaId}`)
    .then(res => res.json())
    .then(mercaderia => {

      let ingredientes = mercaderiaDiv.querySelector(".ingredientes");
      let preparacion = mercaderiaDiv.querySelector(".preparacion");
      let tipo = mercaderiaDiv.querySelector(".tipo");


      if (ingredientes && preparacion && tipo) {
        const ingredientesHeight = ingredientes.scrollHeight + "px";
        const preparacionHeight = preparacion.scrollHeight + "px";
        let tipoHeight = tipo.scrollHeight + "px";

        ingredientes.style.height = ingredientesHeight;
        preparacion.style.height = preparacionHeight;
        tipo.style.height = tipoHeight;

        setTimeout(() => {
          ingredientes.style.height = "0";
          preparacion.style.height = "0";
          tipo.style.height = "0";

          setTimeout(() => {
            ingredientes.remove();
            preparacion.remove();
            tipo.remove();
          }, 300);
        }, 10);
      } else {
        let ingredientes = document.createElement("p");
        let preparacion = document.createElement("p");
        let tipo = document.createElement("p");

        ingredientes.classList.add("ingredientes");
        preparacion.classList.add("preparacion");
        tipo.classList.add("tipo");

        ingredientes.innerHTML = `<strong>Ingredientes: </strong> ${mercaderia.ingredientes}`;
        preparacion.innerHTML = `<strong>Preparación: </strong> ${mercaderia.preparacion}`;
        tipo.innerHTML = `<strong>Tipo: </strong> ${mercaderia.tipo.descripcion}`;

        mercaderiaDiv.querySelector("p").insertAdjacentElement("afterend", preparacion);
        mercaderiaDiv.querySelector("p").insertAdjacentElement("afterend", ingredientes);
        mercaderiaDiv.querySelector("p").insertAdjacentElement("afterend", tipo);

        const ingredientesHeight = ingredientes.scrollHeight + "px";
        const preparacionHeight = preparacion.scrollHeight + "px";
        const tipoHeight = tipo.scrollHeight + "px";

        ingredientes.style.height = "0";
        preparacion.style.height = "0";
        tipo.style.height = "0";

        setTimeout(() => {
          ingredientes.style.height = ingredientesHeight;
          preparacion.style.height = preparacionHeight;
          tipo.style.height = tipoHeight;
        }, 10);
      }
    });
}

let orden = null;
let tipo = null;
let nombreMercaderia = null;

function filtrarMercaderia(tipo, nombreMercaderia, orden) {
  let urlFiltro = `https://localhost:7246/api/v1/Mercaderia?`;
  if (orden) {
    urlFiltro += `orden=${orden}`;
  }
  if (nombreMercaderia) {
    if (orden) {
      urlFiltro += `&`;
    }
    urlFiltro += `nombre=${nombreMercaderia}`;
  }
  if (tipo) {
    if (orden || nombreMercaderia) {
      urlFiltro += `&`;
    }
    urlFiltro += `tipo=${tipo}`;
  }

  let mercaderiaSearch = document.getElementById('carta');
  fetch(urlFiltro)
    .then(res => res.json())
    .then(mercaderias => {
      mercaderiaSearch.innerHTML = "";
      mercaderias.forEach(mercaderia => {


        let detalleDiv = document.createElement("div");
        let nombre = document.createElement("h2");
        let precio = document.createElement("p");
        let imagen = document.createElement("img");
        let botonDetalles = document.createElement("button");
        let botonPedir = document.createElement("button");

        nombre.innerText = mercaderia.nombre;
        console.log(mercaderia.nombre);
        precio.innerHTML = `<strong>Precio: $</strong> ${mercaderia.precio}`;
        imagen.setAttribute("src", mercaderia.imagen);
        botonDetalles.innerText = "Detalles";
        botonPedir.innerText = "Pedir";

        botonDetalles.classList.add("boton_detalles");
        botonPedir.classList.add("boton_pedir");

        botonDetalles.addEventListener("click", () => {
          mostrarDetalle(mercaderia.id, detalleDiv);
        });

        botonPedir.addEventListener("click", () => {
          listarMercaderia(mercaderia.nombre, mercaderia.precio, mercaderia.imagen, mercaderia.id);
        });

        detalleDiv.appendChild(nombre);
        detalleDiv.appendChild(precio);
        detalleDiv.appendChild(imagen);
        detalleDiv.appendChild(botonDetalles);
        detalleDiv.appendChild(botonPedir);


        mercaderiaSearch.appendChild(detalleDiv);
      })


    }) 
};

let buscador = document.getElementById("search");
buscador.addEventListener("input", function () {
  nombreMercaderia = buscador.value.trim();
  filtrarMercaderia(tipo, nombreMercaderia, orden);
  if (nombreMercaderia.length === 0) {
    filtrarMercaderia(tipo, nombreMercaderia, orden);
  }
});

// FILTRAR POR TIPO DE MERCADERIA

const botonesFiltroTipo = document.querySelectorAll('.filtro_tipo');

botonesFiltroTipo.forEach(boton => {
  boton.addEventListener('click', () => {
    botonesFiltroTipo.forEach(btn => btn.classList.remove("tipo_selected"));
    botonMostrarTodas.classList.remove("tipo_selected");
    boton.classList.add("tipo_selected");
    tipo = parseInt(boton.dataset.tipo);
    filtrarMercaderia(tipo, nombreMercaderia, orden);
  });
});

const botonMostrarTodas = document.querySelector('.filtrar_todas');

botonMostrarTodas.addEventListener('click', () => {
  botonesFiltroTipo.forEach(btn => btn.classList.remove("tipo_selected"));
  botonMostrarTodas.classList.add("tipo_selected");
  tipo = null;
  filtrarMercaderia(tipo, nombreMercaderia, orden);
});
//FILTRAR POR ORDEN

const botonesOrden = document.querySelectorAll('.orden');

botonesOrden.forEach(boton => {
  boton.addEventListener('click', () => {
    botonesOrden.forEach(btn => btn.classList.remove("orden_selected"));
    boton.classList.add("orden_selected");
    orden = boton.dataset.orden;
    filtrarMercaderia(tipo, nombreMercaderia, orden);
  });
});



//CREAR COMANDA

function CrearComanda(listaMercaderias, formaEntrega) {
  const url = 'https://localhost:7246/api/v1/Comanda';
  const options = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      mercaderias: listaMercaderias,
      formaEntrega: formaEntrega
    })
  };

  fetch(url, options)
    .then(res => res.json())
    .then(mercaderias => {
    });
}


const formaEntregaButtons = document.querySelectorAll(".forma_entrega");
let forma = "";

formaEntregaButtons.forEach(boton => {
  boton.addEventListener("click", () => {
    formaEntregaButtons.forEach(btn => btn.classList.remove("selected"));
    boton.classList.add("selected");
    forma = parseInt(boton.dataset.forma);
  });
});

//ACTUALIZAR PRECIO

function actualizarPrecioTotal() {
  let total = 0;
  const listaComanda = document.getElementById("lista_comanda").getElementsByTagName("div");

  Array.from(listaComanda).forEach(div => {
    let textoDiv = div.querySelector(".texto_info_pedido");
    let precioElement = div.querySelector("p");
    let cantidadElement = textoDiv ? textoDiv.querySelector("span") : null;

    if (cantidadElement) {
      let cantidad = parseInt(cantidadElement.innerText);
      const precio = parseInt(precioElement.textContent.trim().replace("$", ""));
      let precioTotal = precio * cantidad;
      total += precioTotal;
    }
  });

  const totalPrecioElement = document.getElementById("total_precio");
  if (total === 0) {
    totalPrecioElement.textContent = "";
  } else {
    totalPrecioElement.textContent = `$${total}`;
  }
}

//FUNCIONES MOSTRAR/OCULTAR

function mostrarImagenFondo() {
  let imagenFondo = document.getElementById("imagen_fondo_pedido");
  imagenFondo.style.display = "block";
  let detallepedido = document.getElementById("detalle_pedido");
  detallepedido.style.display = "none";

}

function ocultarImagenFondo() {
  let imagenFondo = document.getElementById("imagen_fondo_pedido");
  imagenFondo.style.display = "none";
  let detallepedido = document.getElementById("detalle_pedido");
  detallepedido.style.display = "block";
}

function pedidoRealizado() {
  let imagenPedido = document.getElementById("pedido_realizado");
  imagenPedido.style.display = "block"
  let detallepedido = document.getElementById("detalle_pedido");
  detallepedido.style.display = "none";
}

function ocultarPedidoRealizado() {
  let imagenPedido = document.getElementById("pedido_realizado");
  imagenPedido.style.display = "none"
}
