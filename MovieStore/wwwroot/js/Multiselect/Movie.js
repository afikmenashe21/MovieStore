﻿$(document).ready(function () {
    $(function () {
        var moviename = document.getElementsByName("Name")[0].value;
        $.ajax({
            url: "/MovieGenres/MultiselectMovie",
            data: { "movie": moviename},
            success: function (dataset) {
                var data = [];
                dataset["data"].forEach(genre => data.push({ "label": genre, "value": genre }));
                var multi = new SelectPure(".multi-select-genres", {
                    options: data,
                    value: dataset["checked"],
                    multiple: true,
                    icon: "fa fa-times",
                    placeholder: "-Please select-",
                    onChange: value => { $('#listgenres')[0].value = value.toString(); },
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

    $(function () {
        var moviename = document.getElementsByName("Name")[0].value;
        $.ajax({
            url: "/MovieActors/MultiselectMovie",
            data: { "movie": moviename },
            success: function (dataset) {
                var data = [];
                    dataset["data"].forEach(actor => data.push({ "label": actor, "value": actor }));
                var multi = new SelectPure(".multi-select-actors", {
                    options: data,
                    value: dataset["checked"],
                    multiple: true,
                    icon: "fa fa-times",
                    placeholder: "-Please select-",
                    onChange: value => { $('#listactors')[0].value = value.toString(); },
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
