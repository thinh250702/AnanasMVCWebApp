$(document).ready(function(){
    // Product Filter
    $(".product-wrapper__filter input[type='checkbox']").click(function(event){
    $(this).parent().toggleClass("checked");
    $(this).siblings("span").toggleClass("checked");
    });
    // Product Card Hover
    var imageName = ""
    $(".product-card").hover(function () {
        imageName = $(this).find("img").attr("src").replace("./public/image/","");
        $(this).find("img").attr("src", "./public/image/product-1.jpg");
        $(this).find("div:first-child > div a").fadeIn({queue: false, duration: 'fast'}).animate({marginBottom: "14px", }, "fast")
    }, function() {
        $(this).find("img").attr("src", `./public/image/${imageName}`);
        $(this).find("div:first-child > div a").fadeOut({queue: false, duration: 'fast'}).animate({marginBottom: "0px"}, "fast")
    })

    // Header Dropdown Menu
    var userNav = $("#user-nav");
    var cartNav = $("#cart-nav");
    var userDropdown = $("#user-dropdown");
    var cartDropdown = $("#cart-dropdown");
    userNav.hover(() => {
        userDropdown.filter(':not(:animated)').slideDown();
    }, () => {
        userDropdown.filter(':not(:animated)').slideUp();
    });
    cartNav.hover(() => {
        cartDropdown.filter(':not(:animated)').slideDown();
    }, () => {
        cartDropdown.filter(':not(:animated)').slideUp();
    });

    // Preview Image
    const prevButton = $(".image-list-wrapper .prevBtn");
    const nextButton = $(".image-list-wrapper .nextBtn");
    const imageList = $(".image-list-wrapper .image-list");
    prevButton.click(() => {
        imageList.animate({scrollLeft: 0}, 500, 'swing')
    })
    nextButton.click(() => {
        imageList.animate({scrollLeft: 1000}, 500, 'swing')
    })

    var sizeRadios = $("input:radio[name='size']");
    sizeRadios.on("change", function(){
        sizeRadios.each(function(index, element){
            $(element).parent().removeClass("active");
        })
        $(this).parent().addClass("active");
    });

    // Vietnam Location Select
    var cities = $("#city-select");
    var districts = $("#district-select");
    var wards = $("#ward-select");
    $.ajax({
        url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
        data: { format: 'json' },
        error: () => {
            console.log("There's something wrong!");
        },
        dataType: "json",
        success: (data) => {
            renderData(data)
        }
    })
    function renderData(data) {
        data.forEach(city => {
            cities.append(new Option(city.Name, city.Id))
        });
        cities.on('change', function() {
            districts.empty().append('<option value="" selected disabled>Quận/Huyện</option>');
            wards.empty().append('<option value="" selected disabled>Phường/Xã</option>');
            if(this.value != ""){
                const dataDistricts = data.filter(n => n.Id === this.value)[0].Districts;
                dataDistricts.forEach(district => {
                    districts.append(new Option(district.Name, district.Id))
                });
            }
        });
        districts.on('change', function() {
            wards.empty().append('<option value="" selected disabled>Phường/Xã</option>');
            const dataCity = data.filter(n => n.Id === cities.val());
            if (this.value != ""){
                const dataWards = dataCity[0].Districts.filter(n => n.Id === this.value)[0].Wards;
                dataWards.forEach(ward => {
                    wards.append(new Option(ward.Name, ward.Id))
                });
            }
        })
    }

    var shippingRadios = $("input:radio[name='shippingMethod']");
    var paymentRadios = $("input:radio[name='paymentMethod']");
    shippingRadios.on("change", function(){
        shippingRadios.each(function(index, element){
            $(element).parent().removeClass("checked");
        })
        $(this).parent().addClass("checked");
    });
    paymentRadios.on("change", function(){
        paymentRadios.each(function(index, element){
            $(element).parent().removeClass("checked");
        })
        $(this).parent().addClass("checked");
    });

    // Dropdown Info
    $(".dropdown-arrow").click(function(){
        var item = $(this).parent().parent();
        item.children("div:last-child").slideToggle();
        $(this).toggleClass("rotate");
        // $(this).parent().toggleClass("active");
    });

    // Dropdown Filter
    $(".dropdown-arrow--filter").click(function(){
        var item = $(this).parent().parent();
        item.children("div:last-child").toggle();
        $(this).toggleClass("rotate");
        // $(this).parent().toggleClass("active");
    });

    // Recommended Product
    const productPrevButton = $(".recomended-wrapper .prevBtn");
    const productNextButton = $(".recomended-wrapper .nextBtn");
    const productList = $(".recomended-product-wrapper");
    productPrevButton.click(() => {
        productList.animate({scrollLeft: "-=1220"}, 'slow')
        console.log("Prev clicked")
    })
    productNextButton.click(() => {
        productList.animate({scrollLeft: "+=1220"}, 'slow')
        console.log("Next clicked")
    })

    var passwordPanel = $("#passwordPanel");
    var emailPanel = $("#emailPanel");
    var leftSubmit = $("#leftSubmit");
    var rightColumn = $(".user-profile__column.change");
    var changePassword = $("input[name='changePassword']");
    var changeEmail = $("input[name='changeEmail']");

    function switchButton(email, passworld) {
        changeEmail.prop("checked", email)
        changePassword.prop("checked", passworld)
        changeEmail.parent().toggleClass("checked");
        changePassword.parent().toggleClass("checked");
        emailPanel.toggleClass("hidden");
        passwordPanel.toggleClass("hidden");
    }

    changePassword.click(function(){
        if (!changeEmail.prop("checked")){
            changePassword.parent().toggleClass("checked");
            passwordPanel.toggleClass("hidden");
            leftSubmit.toggleClass("hidden");
            rightColumn.toggleClass("hidden");
        } else {
            switchButton(false, true)
        }
    });
    changeEmail.click(function(){
        if (!changePassword.prop("checked")){
            changeEmail.parent().toggleClass("checked");
            emailPanel.toggleClass("hidden");
            leftSubmit.toggleClass("hidden");
            rightColumn.toggleClass("hidden");
        } else {
            switchButton(true, false)
        }
    });
});