# Blazor.DragDrop
This component provides simple and user-friendly drag and drop functionality for Blazor applications.

## Features
- Drag and drop items within a list, both horizontally and vertically.
- Drag and drop items between multiple lists. (Coming soon)
- Choose any HTML element to serve as the parent.
- Apply custom CSS classes to the parent element.
- Use a drag handle to allow dragging only by the handle, rather than the whole item.
- Prevent specific items from being dragged.
- Prevent reordering the list.

## How to use
1. Install the **RMN.Blazor.DragDrop** NuGet package in your project.

2. Add the component namespace to your `_Imports.razor` file:
```razor
@using RMN.Blazor.DragDrop
```

3. Add **SortableJS**:
```html
<script src="https://cdn.jsdelivr.net/npm/sortablejs@latest/Sortable.min.js"></script>
```

## API

### Properties
|Name                |Type         |Default|Description|
|--------------------|-------------|-------|-----------|
|RootElement         |String       |"div"  |Sets what element to serve as the parent.|
|Id                  |String       |Guid   |Id for the parent element.|
|Class               |String       |""     |CSS class for the parent element.|
|DragHandleClass     |String       |""     |CSS class for the drag handle.|
|UndraggableItemClass|String       |""     |CSS class for items that can't be dragged.|
|AllowReorder        |Boolean      |true   |Enables or disables reordering the list.|
|Items               |List&lt;T&gt;|[ ]    |List of items.|
|Context             |String       |context|Sets the parameter name for the list items.|

### Events
`OnUpdate`: The method to be called when the list is updated.

## Examples

### Basic example
```html
<DragDropList Items="Items" Context="item">
    <DragDropListItem>
        <p>@item.Name</p>
    </DragDropListItem>
</DragDropList>
```

### Advanced example
```html
<DragDropList RootElement="ul"
              Id="dragDropList"
              Class="drag-drop-list"
              DragHandleClass="drag-handle"
              UndraggableItemClass="not-draggable"
              Items="Items"
              Context="item"
              OnUpdate="OnUpdate">

    <DragDropListItem>
        <li>
            <i class="fa-solid fa-grip-vertical drag-handle @(item.Disabled ? "not-draggable" : "")"></i>
            <span>@item.Name</span>
        </li>
    </DragDropListItem>

</DragDropList>
```

## Licence
Project is licensed under the [MIT License](https://github.com/aarm1n/Blazor.DragDrop/blob/main/LICENSE).