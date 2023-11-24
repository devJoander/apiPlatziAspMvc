function createProduct() {
    var imageUrl = "https://i.ibb.co/bHSrDjd/cell.jpg";

    var productData = {
        title: $("#inputTitle").val(),
        price: $("#inputPrice").val(),
        description: $("#inputDescription").val(),
        categoryId: $("#selectCategory").val(),
        // images: [$("#customImage").val()]
        images: [imageUrl]
        // images: []
    };
    console.log("productData ",productData);
    $.ajax({
        url: '@Url.Action("CreateProduct", "Home")', // Asegúrate de reemplazar "TuControlador" con el nombre de tu controlador
        type: 'POST',
        contentType: 'application/json',    
        data: JSON.stringify(productData),
        success: function (result) {
            // Manejar el éxito de la creación del producto si es necesario
            window.location.reload(); // Recargar la página o realizar otra acción
            console.log(result);
            $('.btnModal').show();
            console.log('@Url.Action("AddProduct", "Home")');
        },
        error: function () {
            // Manejar el error de la creación del producto si es necesario
            alert('Error al crear el producto.');
        }
    });
}

//<div class="row" id="productsContainer">
//    @foreach (var product in Model.Products)
//    {
//        <div class="col-md-4">
//            <div class="card" style="width: 100%; height:100%; border-radius:3vh; border: solid 0.2vh #000; padding:3vh;">
//                <img src="@product.images[0]" style="width: 18vh; height:18vh;" class="card-img-top" alt="Product Image">
//                    <div class="card-body">
//                        <h5 class="card-title">@product.title</h5>
//                        <h6 class="card-subtitle mb-2 text-muted">@("Price: " + product.price)</h6>
//                        <p class="card-text">@product.description</p>
//                        <p class="card-text">@product.id</p>
//                        <!-- Botones en las cards -->
//                        <button type="button" class="btn btn-success btnVer" onclick="getProductDetailsById(@product.id, 'ver')">See more</button>
//                        <button type="button" class="btn btn-warning btnUp" onclick="getProductDetailsById(@product.id, 'editar')">Update</button>
//                        <button type="button" class="btn btn-danger btnDe" onclick="getProductDetailsById(@product.id, 'eliminar')">Delete</button>
//                    </div>
//            </div>
//            </div>
//    }
//        </div>