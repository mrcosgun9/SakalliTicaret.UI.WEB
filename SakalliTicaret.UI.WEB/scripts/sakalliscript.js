$("#UserCreate")
    .validate({
        rules: {
            Name: "required",
            LastName: "required",
            Telephone: {
                required: true,
                minlength: 10,
                maxlength: 11
            },
            Email: {
                required: true,
                email: true
            },
            Password: "required"
        }
    });
$(".basketCreate").click(function myfunction() {
    var uid = $(this).attr("name");
    $.ajax({
        type: "GET",
        url: "/Basket/Create",
        data: { productId: uid },
        success: function myfunction(count) {
            $("#basketCount").html(count);
            $.ajax({
                type: "GET",
                url: "/Basket/MiniBasketWidget",
                success: function sVeriGetir(veri) {
                    $(".basketdiv").html('');
                    $(".basketdiv").html(veri);
                    $("#shoppingcart").addClass("shake");


                    toastr.success('Ürün Sepete Başarıyla Eklendi');

                }
            });
        },
        error: function myfunction() {
            toastr.warning('Ürün Sepete Eklenemedi.Bizimle İletişime Geçin Lütfen.');
        }
    });
    $("#shoppingcart").removeClass("shake");
});
$("#basketListOpen").click(function () {
    $(".basketdiv").animate({
        width: "toggle"
    });
});
$(function () {
    $(":input[data-autocomplete]").each(function () {
        $(this).autocomplete({
            source: $(this).attr("data-autocomplete")
        });
    });

});
$(document).ready(function () {
    $('.date').mask('00/00/0000');
    $('.time').mask('00:00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 0000-0000');
    $('.phone_us').mask('00000000000');
    $('.mixed').mask('AAA 000-S0S');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true });
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
    $('.money2').mask("#.##0,00", { reverse: true });
    $('.ip_address').mask('0ZZ.0ZZ.0ZZ.0ZZ', {
        translation: {
            'Z': {
                pattern: /[0-9]/, optional: true
            }
        }
    });
    $('.ip_address').mask('099.099.099.099');
    $('.percent').mask('##0,00%', { reverse: true });
    $('.clear-if-not-match').mask("00/00/0000", { clearIfNotMatch: true });
    $('.placeholder').mask("00/00/0000", { placeholder: "__/__/____" });
    $('.fallback').mask("00r00r0000", {
        translation: {
            'r': {
                pattern: /[\/]/,
                fallback: '/'
            },
            placeholder: "__/__/____"
        }
    });
    $('.selectonfocus').mask("00/00/0000", { selectOnFocus: true });
});

$(document).ready(function () {
    $("nav ul.xt-side-menu li").hover(
        function () {
            $(this).children("ul").stop(true, true).slideDown();

        },
        function () {
            $(this).children("ul").stop(true, true).slideUp();
        });
});