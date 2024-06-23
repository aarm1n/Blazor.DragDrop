export function init(id, sort, handle, filter, component) {
    new Sortable(document.getElementById(id), {
        sort: sort,
        handle: handle,
        filter: filter,
        forceFallback: true,
        animation: 200,
        onUpdate: (event) => {
            event.item.remove();
            event.to.insertBefore(event.item, event.to.childNodes[event.oldIndex]);

            component.invokeMethodAsync('OnUpdateJS', event.oldDraggableIndex, event.newDraggableIndex);
        }
    });
}