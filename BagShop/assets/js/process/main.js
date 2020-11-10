const BASE_URL = 'http://localhost:51420/api';
// const BASE_URL = 'http://10.5.50.10:51420/api';
// const BASE_IMG = 'http://10.5.50.10:51420/images/';
const BASE_IMG = 'http://localhost:51420/images/';


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#img').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

const getProduct = () => {
    axios.get(`${BASE_URL}/products`).then((result) => {
        var product = result.data;
        product.forEach(element => {
            $("#product").append("<div class='col-xl-4 col-lg-4 col-md-6 col-sm-6'> "
                + " <div class='single-popular-items mb-50 text-center'> "
                + " <div class='popular-img'> "
                + " <img src='" +BASE_IMG+ element.image + "' > "
                + " <div class='img-cap' > "
                + " <span onclick='addToCart(" + JSON.stringify(element) + ")'> Add to cart</span>"
                + " </div > "
                + " <div class='favorit-items' > "
                + " <span class='flaticon-heart' ></span > "
                + " </div >"
                + " </div >"
                + " <div class='popular-caption'> "
                + " <h3><a href='product_details.html'>" + element.productName + "</a></h3>"
                + " <span>" + element.price + " vnd </span>"
                + " </div>"
                + " </div >"
                + " </div >");
        });

        product.forEach(element => {
            $("#updateProduct").append("<div class='col-xl-4 col-lg-4 col-md-6 col-sm-6'> "
                + " <div class='single-popular-items mb-50 text-center'> "
                + " <div class='popular-img'> "
                + " <img src='" + BASE_IMG + element.image + "' > "
                + " <div class='img-cap' > "
                + " <span><a href='bag.html'>Update</a></span>"
                + " </div > "
                + " <div class='favorit-items' > "
                + " <span class='flaticon-heart' ></span > "
                + " </div >"
                + " </div >"
                + " <div class='popular-caption'> "
                + " <h3><a href='product_details.html'>" + element.productName + "</a></h3>"
                + " <span>" + element.price + " vnd </span>"
                + " </div>"
                + " </div >"
                + " </div >");
        });
    }).catch((err) => {
        console.error(err);
    });
};
getProduct();
const addToCart = (product) => {
    //Check Session Storage xem có sản phẩm nào chưa;
    let listCart = JSON.parse(sessionStorage.getItem("cart"));
    if (listCart == null) {
        let listCart = [{ product: product, quantity: 1 }];
        sessionStorage.setItem("cart", JSON.stringify(listCart));
    }
    else {
        let flag = false;
        //Check xem product da co trong list chua
        for (let i = 0; i < listCart.length; i++) {
            if (product.productID == listCart[i].product.productID) {
                listCart[i].quantity++;
                sessionStorage.setItem("cart", JSON.stringify(listCart));
                flag = true;
            }
        }
        if (!flag) {
            let newProduct = { product: product, quantity: 1 };
            listCart.push(newProduct);
            sessionStorage.setItem("cart", JSON.stringify(listCart));
        }

    }
}

const getCart = () => {
    let listCart = JSON.parse(sessionStorage.getItem("cart"));
    if (listCart != null) {

        listCart.forEach(element => {
            $("#ListProductInCart").append("<tr> <td> <div class='media'> <div class='d-flex'> <img src='"+BASE_IMG+ element.image + "'/> "
                + " </div> <div class='media-body'> <p>" + element.product.productName + "</p> </div > </div > </td > <td> <h5>"
                + element.product.price + " vnd</h5> </td > <td> <div class='product_count'>  "
                + " <input class='input-number' id='update' onchange='updateCart(" + JSON.stringify(element.product) + ")' type='number' value=" + element.quantity + "> "
                + " </div> </td> <td> "
                + " <h5>" + element.product.price * element.quantity + " vnd </h5 > </td > "
                + " <td> <button class='btn btn-sm btn-danger' onclick='deleteProductInCart(" + JSON.stringify(element.product) + ")' type='submit'><i class='fa fa-trash'></i></button> "
                + " </td> </tr > ");
        });
    }
}
getCart();

const deleteProductInCart = (product) => {
    let listCart = JSON.parse(sessionStorage.getItem("cart"));
    if (listCart != null) {
        for (i = 0; i < listCart.length; i++) {
            if (listCart[i].product.productID === product.productID) {
                listCart.splice(i, 1);
            }
        }
        if (listCart.length == 0) {
            listCart = null;
        }
        sessionStorage.setItem("cart", JSON.stringify(listCart));
        location.reload();
    }

}

const updateCart = (product) => {
    let newAmount = parseInt(document.getElementById("update").value);
    let listCart = JSON.parse(sessionStorage.getItem("cart"));
    if (listCart != null) {
        for (let i = 0; i < listCart.length; i++) {
            if (product.productID == listCart[i].product.productID && listCart[i].product.quantity >= newAmount) {
                listCart[i].quantity = newAmount;
                sessionStorage.setItem("cart", JSON.stringify(listCart));
            }
        }
        location.reload();
    }
}

const calculateTotal = () => {
    let listCart = JSON.parse(sessionStorage.getItem("cart"));
    let total = 0;
    if (listCart != null) {
        for (let i = 0; i < listCart.length; i++) {
            total += listCart[i].product.price * listCart[i].quantity;
        }
    }
    document.getElementById("totalPrice").innerHTML = total;
    return total;
}

if ((listCart = JSON.parse(sessionStorage.getItem("cart"))) != null) {
    calculateTotal();
}

function submitform(){
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "http://localhost:51420/api/Orders", true);
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    var order = {
        customerName : document.getElementById("cusName").value,
        customerAddress : document.getElementById("cusAddress").value,
        customerPhone : document.getElementById("cusPhone").value,
        OrderDetails : JSON.parse(sessionStorage.getItem("cart")),
        total : document.getElementById("totalPrice").value,
    }
    xhr.onload = (event) =>{
        let status = xhr.status;
        console.log(status);
        if(status == 200){
            sessionStorage.removeItem("cart");
        }else{
            alert("Something went Wrong");
        }
    }
    console.log(JSON.stringify(order));
    xhr.send(JSON.stringify(order));


}


