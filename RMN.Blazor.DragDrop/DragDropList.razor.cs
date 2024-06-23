using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMN.Blazor.DragDrop
{
    public partial class DragDropList<T>
    {
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }



        [Parameter]
        public string RootElement { get; set; } = "div";

        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Parameter]
        public string Class { get; set; } = string.Empty;

        [Parameter]
        public string DragHandleClass { get; set; } = string.Empty;

        [Parameter]
        public string UndraggableItemClass { get; set; } = string.Empty;

        [Parameter]
        public bool AllowReorder { get; set; } = true;

        [Parameter]
        public List<T> Items { get; set; } = new List<T>();

        [Parameter]
        public EventCallback OnUpdate { get; set; }

        [Parameter]
        public RenderFragment<T>? DragDropListItem { get; set; }

        private DotNetObjectReference<DragDropList<T>>? selfReference;



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                selfReference = DotNetObjectReference.Create(this);

                var module = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "./_content/RMN.Blazor.DragDrop/DragDropList.razor.js");

                await module.InvokeAsync<string>(
                    "init",
                    Id,
                    AllowReorder,
                    !string.IsNullOrEmpty(DragHandleClass) ? $".{DragHandleClass}" : "",
                    !string.IsNullOrEmpty(UndraggableItemClass) ? $".{UndraggableItemClass}" : "",
                    selfReference
                );
            }
        }

        private RenderFragment RenderContent() => builder =>
        {
            builder.OpenElement(0, RootElement);

            builder.AddMultipleAttributes(1, new Dictionary<string, object>
            {
                { "id", Id },
                { "class", Class }
            });

            foreach (var item in Items)
                builder.AddContent(2, DragDropListItem!(item));

            builder.CloseElement();
        };

        [JSInvokable]
        public void OnUpdateJS(int oldIndex, int newIndex)
        {
            var itemToMove = Items[oldIndex];

            Items.RemoveAt(oldIndex);
            Items.Insert(newIndex, itemToMove);

            StateHasChanged();

            OnUpdate.InvokeAsync();
        }
    }
}