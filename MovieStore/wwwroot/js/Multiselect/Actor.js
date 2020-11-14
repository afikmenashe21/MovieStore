$(document).ready(function () {
    $(function () {
        var actorname = document.getElementsByName("Name")[0].value;
        $.ajax({
            url: "/MovieActors/MultiselectActor",
            data: { "actor": actorname},
            success: function (dataset) {
                var data = [];
                dataset["data"].forEach(actor => data.push({ "label": actor, "value": actor }));
                var multi = new SelectPure(".multi-select-movies", {
                    options: data,
                    value: dataset["checked"],
                    multiple: true,
                    icon: "fa fa-times",
                    placeholder: "-Please select-",
                    onChange: value => { $('#listmovies')[0].value = value.toString(); },
                    classNames: {
                        select: "select-pure__select",
                        dropdownShown: "select-pure__select--opened",
                        multiselect: "select-pure__select--multiple",
                        label: "select-pure__label",
                        placeholder: "select-pure__placeholder",
                        dropdown: "select-pure__options",
                        option: "select-pure__option",
                        autocompleteInput: "select-pure__autocomplete",
                        selectedLabel: "select-pure__selected-label",
                        selectedOption: "select-pure__option--selected",
                        placeholderHidden: "select-pure__placeholder--hidden",
                        optionHidden: "select-pure__option--hidden",
                    }
                });
            }
        });
    })

})
