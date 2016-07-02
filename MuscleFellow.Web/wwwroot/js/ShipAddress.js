$(document).ready(function () {
    $("#dpProvince").change(function () {
        var url = "/ShipAddress/GetCities?ProvinceID=" + $("#dpProvince").val();
        $.getJSON(url, "", function (data) {
            $('#dpCities').html('');
            $.each(data, function (i, item) {
                $('#dpCities').append($("<option></option>").val(item.ID).html(item.Name));
            });
        });
    });
});