//using microsoft.aspnetcore.mvc;
//using soldaline_back.models;
//using system.threading.tasks;
//using microsoft.entityframeworkcore;
//using soldaline_back.dtos;

//namespace soldaline_back.controllers
//{
//    [route("api/[controller]")]
//    [apicontroller]
//    public class proveedorcontroller : controller
//    {
//        private readonly soldalinebdcontext _context;

//        public proveedorcontroller(soldalinebdcontext context)
//        {
//            _context = context;
//        }

//        // método para registrar un nuevo proveedor
//        [httppost("register")]
//        public async task<iactionresult> register([frombody] proveedorregisterdto proveedordto)
//        {
//            if (proveedordto == null)
//            {
//                return badrequest("datos inválidos.");
//            }

//            // verificar si el proveedor ya está registrado por el nombre de la empresa o teléfono de contacto
//            var existeproveedor = await _context.proveedors
//                .anyasync(p => p.nombreempresa == proveedordto.nombreempresa || p.telefonocontacto == proveedordto.telefonocontacto);

//            if (existeproveedor)
//            {
//                return badrequest("proveedor ya registrado.");
//            }

//            // crear el objeto proveedor a partir del dto
//            var proveedor = new proveedor
//            {
//                nombreempresa = proveedordto.nombreempresa,
//                direccion = proveedordto.direccion,
//                telefonocontacto = proveedordto.telefonocontacto,
//                nombrecontacto = proveedordto.nombrecontacto,
//                apellidom = proveedordto.apellidom,
//                apellidop = proveedordto.apellidop,
//                estatus = 1 // proveedor activo por defecto
//            };

//            // guardar el proveedor en la base de datos
//            _context.proveedors.add(proveedor);
//            await _context.savechangesasync();

//            return ok("proveedor registrado exitosamente.");
//        }

//        // método para obtener los detalles de un proveedor
//        [httpget("{id}")]
//        public async task<iactionresult> getproveedor(int id)
//        {
//            var proveedor = await _context.proveedors.firstordefaultasync(p => p.id == id);

//            if (proveedor == null)
//            {
//                return notfound("proveedor no encontrado.");
//            }


//            // crear un dto para la respuesta
//            var proveedorresponse = new proveedorresponsedto
//            {
//                id = proveedor.id,
//                nombreempresa = proveedor.nombreempresa,
//                direccion = proveedor.direccion,
//                telefonocontacto = proveedor.telefonocontacto,
//                nombrecontacto = proveedor.nombrecontacto,
//                apellidom = proveedor.apellidom,
//                apellidop = proveedor.apellidop,
//                estatus = proveedor.estatus
//            };

//            return ok(proveedorresponse);
//        }
//    }
//}
