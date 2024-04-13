$(document).ready(function(){
    // INDEX:
    // ---Header Dropdown Menu
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

    // PRODUCT LIST
    // ---Product Filter
    $(".product-wrapper__filter input[type='checkbox']").click(function(event){
        $(this).parent().toggleClass("checked");
        $(this).siblings("span").toggleClass("checked");
    });
    // ---Product Card Hover
    $(".product-card").hover(function () {
        /*$(this).find("img.normal").hide();
        $(this).find("img.hover").show();*/
        $(this).find("img.hover").animate({ opacity: '1' });
        $(this).find("div:first-child > div:first-child a").fadeIn({queue: false, duration: 'fast'}).animate({marginBottom: "14px", }, "fast")
    }, function() {
        /*$(this).find("img.normal").show();
        $(this).find("img.hover").hide();*/
        $(this).find("img.hover").animate({ opacity: '0' });
        $(this).find("div:first-child > div:first-child a").fadeOut({queue: false, duration: 'fast'}).animate({marginBottom: "0px"}, "fast")
    })
    // ---Dropdown Filter
    $(".dropdown-arrow--filter").click(function(){
        var item = $(this).parent().parent();
        item.children("div:last-child").toggle();
        $(this).toggleClass("rotate");
        // $(this).parent().toggleClass("active");
    });

    // PRODUCT DETAIL
    // ---Change preview image on click
    var imageDisplay = $("#image-display");
    $(".image-item").click(function () {
        imageDisplay.attr("src", $(this).attr("src"))
    });
    // ---Preview Image Slider
    const prevButton = $(".image-list-wrapper .prevBtn");
    const nextButton = $(".image-list-wrapper .nextBtn");
    const imageList = $(".image-list-wrapper .image-list");
    prevButton.click(() => {
        imageList.animate({scrollLeft: 0}, 500, 'swing')
    })
    nextButton.click(() => {
        imageList.animate({scrollLeft: 1000}, 500, 'swing')
    })
    // ---Dropdown Info
    $(".dropdown-arrow").click(function(){
        var item = $(this).parent().parent();
        item.children("div:last-child").slideToggle();
        $(this).toggleClass("rotate");
        // $(this).parent().toggleClass("active");
    });
    // ---Size Radio Button
    var sizeRadios = $("input:radio[name='size']");
    sizeRadios.on("change", function(){
        sizeRadios.each(function(index, element){
            $(element).parent().removeClass("active");
        })
        $(this).parent().addClass("active");
    });
    
    // CHECKOUT
    //  ---Vietnam Location Select
    var cities = $("select[name='Province']");
    var districts = $("select[name='District']");
    var wards = $("select[name='Ward']");
    var apiToken = "760d5095-f700-11ee-893f-b6ed573185af";
    $.ajax({
        //url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
        url: "https://online-gateway.ghn.vn/shiip/public-api/master-data/province",
        headers: { "Token": apiToken },
        dataType: "json",
        success: (response) => {
            response.data.forEach(
                city => { cities.append(new Option(city.ProvinceName, city.ProvinceID)) }
            );
        },
        error: () => {
            alert("Failed to load province data!");
        }
    })
    cities.change(function () {
        districts.empty().append('<option value="" selected disabled>Quận/Huyện</option>');
        wards.empty().append('<option value="" selected disabled>Phường/Xã</option>');
        if (this.value != "") {
            $("input[name='ProvinceName']").val($("select[name='Province'] option:selected").text())
            $.ajax({
                url: "https://online-gateway.ghn.vn/shiip/public-api/master-data/district",
                headers: { "Token": apiToken },
                data: { province_id: this.value },
                dataType: "json",
                success: (response) => {
                    response.data.forEach(
                        district => { districts.append(new Option(district.DistrictName, district.DistrictID)) }
                    );
                },
                error: () => {
                    alert("Failed to load district data!");
                }
            });
        }
    })
    districts.change(function () {
        wards.empty().append('<option value="" selected disabled>Phường/Xã</option>');
        if (this.value != "") {
            $("input[name='DistrictName']").val($("select[name='District'] option:selected").text())
            $.ajax({
                url: "https://online-gateway.ghn.vn/shiip/public-api/master-data/ward",
                headers: { "Token": apiToken },
                data: { district_id: this.value },
                dataType: "json",
                success: (response) => {
                    response.data.forEach(
                        ward => { wards.append(new Option(ward.WardName, ward.WardCode)) }
                    );
                },
                error: () => {
                    alert("Failed to load ward data!");
                }
            });
        }
    })
    wards.change(function () {
        if (this.value != "") {
            $("input[name='WardName']").val($("select[name='Ward'] option:selected").text())
        }
    });

    // ---Checkout Radio Option
    var shippingRadios = $("input:radio[name='ShippingMethod']");
    var paymentRadios = $("input:radio[name='PaymentMethod']");
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

    // ---Recommended Product Slider (INDEX & PRODUCT DETAIL)
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

    // PROFILE
    // ---Toggle Email and Password Panel
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

    $(".order-row .toggle-button").click(function () {
        $(this).parent().parent().find(".order-row__body").toggle();
        $(this).find("i").toggleClass("rotate");
    })

    $("input[type='password']").focus(function () {
        $(this).css({
            "transform": "translate(0, -3px)",
            "box-shadow": "0 3px 0 0 var(--neutral-0)",
            "transition": "0.3s"
        })
        $(this).parent().find("span").css({
            "transform": "translateY(calc(-3px - 50%))",
            "transition": "0.3s"
        })
        console.log("on focus");
    });
    $("input[type='password']").blur(function () {
        $(this).css({
            "transform": "translate(0, 0)",
            "box-shadow": "none",
        });
        $(this).parent().find("span").css("transform", "translateY(-50%)")
    });
    $(".password-wrapper span i").click(function () {
        if ($(this).hasClass("fa-eye")) {
            $(this).parent().parent().find("input[type='password']").attr("type", "text")
        } else {
            $(this).parent().parent().find("input[type='text']").attr("type", "password")
        }
        $(this).toggleClass("fa-eye").toggleClass("fa-eye-slash")
    })

    var nextHeight = $(".toast-msg").width() + 20;
    $(".toast-msg").animate({ left: nextHeight }, "slow");
    setTimeout(function () {
        $(".toast-msg").animate({ left: "-=40%" }, "slow");
    }, 2000);
    $(".toast-msg i:last-child").click(function () {
        $(this).parent().animate({ left: "-=40%" }, "slow");
    })
});