
function hideAllTabs() {
    var tabs = document.querySelectorAll(".nav-link");
    var tabContents = document.querySelectorAll(".tab-pane");

    tabs.forEach(function (tab) {
        tab.classList.remove("active");
        tab.setAttribute("aria-selected", "false");
    });

    tabContents.forEach(function (content) {
        content.classList.remove("show", "active");
    });
}

function showAndActivateTab(tabName) {
    hideAllTabs();

    var tab = document.getElementById(tabName + "-tab");
    var content = document.getElementById(tabName);

    if (tab && content) {
        tab.classList.add("active");
        content.classList.add("show", "active");
        tab.setAttribute("aria-selected", "true");
    }
}

document.addEventListener("DOMContentLoaded", function () {
    var urlParams = new URLSearchParams(window.location.search);
    var tabToOpen = urlParams.get("tab");

    // Show and activate the default "Orders" tab
    var ordersTab = document.getElementById("rents-tab");
    var ordersContent = document.getElementById("rents");

    ordersTab.classList.add("active");
    ordersContent.classList.add("show", "active");
    if (['comments', 'rents', 'events'].indexOf(tabToOpen) == -1) {
        return;
    }

     showAndActivateTab(tabToOpen);
   
});