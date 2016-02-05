ko.bindingHandlers["remoteOptions"] = {
    'init': function(element, valueAccessor, allBindings) {
        var options = ko.observableArray([]);
        allBindings.get('value')['__remoteDropdown_options'] = options;

        var binding = valueAccessor();
        var parametersChangeListener = ko.computed(function() {
            var result = {};
            for (var parameterName in binding.parameters || {}) {
                result[parameterName] = ko.unwrap(binding.parameters[parameterName]);
            }
            return result;
        });
        parametersChangeListener.subscribe(refreshOptions);
        if (!binding.parameters) refreshOptions();

        function refreshOptions(parameters) {
            $.when($.getJSON(binding.url, parameters))
                .then(options);
        };

        ko.bindingHandlers['options'].init(element);
    },

    'update': function(element, valueAccessor, allBindings, bindingContext) {
        
        function optionsValueAccessor() {
            return allBindings.get('value')['__remoteDropdown_options'];
        }

        ko.bindingHandlers['options'].update(element, optionsValueAccessor, allBindings);
    }
}