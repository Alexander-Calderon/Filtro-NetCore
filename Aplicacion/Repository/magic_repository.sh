#!/bin/bash

# Archivo original
original=CargoRepository.cs

# Array con los nuevos nombres  
nombres=(Cliente Color Departamento DetalleOrden DetalleVenta Empleado Empresa Estado FormaPago Genero Insumo Inventario InventarioTalla Municipio Orden Pais Prenda Proveedor Talla TipoEstado TipoPersona TipoProteccion Venta)

# Palabra a reemplazar
reemplazo=Cargo

for nombre in "${nombres[@]}"
do
  # Duplicar el archivo original
  cp "$original" "${nombre}Repository.cs"

  # Reemplazar la palabra en el archivo duplicado
  sed -i "s/$reemplazo/$nombre/g" "${nombre}Repository.cs" 
done

