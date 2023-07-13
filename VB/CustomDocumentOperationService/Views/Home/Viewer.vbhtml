<style>
    .custom-image-item {
        background-image: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4IiB2aWV3Qm94PSIwIDAgMjYgMjYiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDI2IDI2OyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSI+PHN0eWxlIHR5cGU9InRleHQvY3NzIj4uc3Qwe2ZpbGw6IzNEM0QzRDt9PC9zdHlsZT48Zz48cGF0aCBjbGFzcz0ic3QwIiBkPSJNMjUsN2MwLTAuMi0wLjEtMC42LTAuMy0wLjdDMjQuNiw2LjEsMjQuNCw2LDI0LjIsNkgxNnY1LjdsMS45LDEuN0wyNSw3eiIvPjxwYXRoIGNsYXNzPSJzdDAiIGQ9Ik0xLDIybDE0LDNWMUwxLDRWMjJ6IE04LDEwYzEuNywwLDMsMS44LDMsNGMwLDIuMi0xLjMsNC0zLDRzLTMtMS44LTMtNEM1LDExLjgsNi4zLDEwLDgsMTB6Ii8+PHBhdGggY2xhc3M9InN0MCIgZD0iTTE4LDE1bC0yLTJsMCw4bDguMiwwYzAuMiwwLDAuNC0wLjEsMC42LTAuM2MwLjItMC4yLDAuMi0wLjQsMC4yLTAuNkwyNSw5TDE4LDE1TDE4LDE1eiIvPjxlbGxpcHNlIGNsYXNzPSJzdDAiIGN4PSI4IiBjeT0iMTQiIHJ4PSIyIiByeT0iMyIvPjwvZz48L3N2Zz4NCg==);
        background-repeat: no-repeat;
    }
</style>
<script>
    function CustomizeMenuActions(s, e) {
        var itemDisabled = ko.observable(true);
        s.Init.AddHandler(function (s) {
            var exportDisabled = s.GetReportPreview().exportDisabled;
            exportDisabled.subscribe(function (newValue) { itemDisabled(newValue); });
            itemDisabled(exportDisabled());
        });
        var sendViaEmailItem = {
            id: 'someCustomId',
            imageClassName: 'custom-image-item',
            text: 'Send via Email',
            visible: true,
            disabled: itemDisabled,
            clickAction: function () {
                s.PerformCustomDocumentOperation('someone@test.com')
                    .done((arg) => alert('Document ' + arg.documentId + " : " + arg.message));
            }
        };
        e.Actions.push(sendViaEmailItem);
    }
</script>
@Html.DevExpress().WebDocumentViewer(
                Sub(settings)
                    settings.Name = "WebDocumentViewer1"
                    settings.ClientSideEvents.CustomizeMenuActions = "CustomizeMenuActions"
                End Sub).Bind("CategoriesReport").GetHtml()
