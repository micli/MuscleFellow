
var AjaxShoppingCart = {
    //Properties
    waitingLoading: false,
    popupNotification: false,
    topCartSelector: '',
    topWishlistSelector: '',
    flyoutCartSelector: '',

    // Initilization
    init: function (popupNotification, topCartSelector, topWishlistSelector, flyoutCartSelector) {
        this.waitingLoading = false;
        this.popupNotification = popupNotification;
        this.topCartSelector = topCartSelector;
        this.topWishlistSelector = topWishlistSelector;
        this.flyoutCartSelector = flyoutCartSelector;
    },

    // Show/Hide Waiting UI when page loading.
    SetWaitingLoading: function (display) {
        displayAjaxLoading(display);
        this.waitingLoading = display;
    },

    //Add a product to the cart from the product details page
    AddProduct2Cart_Details: function (addUrl) {
        if (this.waitingLoading != false) {
            return;
        }
        this.SetWaitingLoading(true);
        $.ajax({
            cache: false,
            url: addUrl,
            type: 'post',
            success: this.ajax_Succeeded,
            complete: this.SetWaitingLoading(false),
            error: this.ajaxFailure
        });
    },

    AddProdcut2Order: function(confirmUrl){
        if (this.waitingLoading != false) {
            return;
        }
        $(location).attr("href", confirmUrl);
    },

    AddCartItems2OrderConfirm : function(confirmUrl){
        if (this.waitingLoading != false) {
            return;
        }
        this.SetWaitingLoading(true);
        var idList = "";
        $(".checkbox-inline").each(function () {
            if (true == $(this).is(':checked'))
                idList += $(this).attr("id") + "|";
        });
        this.SetWaitingLoading(false);
        if (idList.length <= 2)
        {
            var msg = {
                message: '请先选择要购买的商品！',
                success: false
            };
            this.ajax_Succeeded(msg);
            return;
        }
        $(location).attr("href", confirmUrl + "?idList=" + idList);
    },

    CheckAllItems: function () {
        var checked = $("#chkall").is(':checked');
        if (checked)
            $(".checkbox-inline").attr("checked", checked);
        else
            $(".checkbox-inline").removeAttr("checked");
    },
    // Ajax results handlers
    ajax_Succeeded: function (response) {
        if (response.message) {
            //display notification
            if (response.success == true) {
                //success
                if (AjaxShoppingCart.usepopupnotifications == true) {
                    displayPopupNotification(response.message, 'success', true);
                }
                else {
                    //specify timeout for success messages
                    displayBarNotification(response.message, 'success', 3500);
                }
            }
            else {
                //error
                if (AjaxShoppingCart.usepopupnotifications == true) {
                    displayPopupNotification(response.message, 'error', true);
                }
                else {
                    //no timeout for errors
                    displayBarNotification(response.message, 'error', 3500);
                }
            }
            return false;
        }
        if (response.redirect) {
            location.href = response.redirect;
            return true;
        }
        return false;
    },

    ajax_Failure: function () {
        alert('添加到购物车失败，请刷新页面再试。');
    }
};