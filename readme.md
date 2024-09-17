# Demo App for showing an issue with livecharts graph library

This App shows that a graph plot is not removing from the view when removing the data from the lists and the axes lists.

## Description and Usage

Scaling a plot collection to a specific YAxes can be done via the property ScalesYAt. This Property is an integer and looks in the connected YAxes List for this Axis.
When removing plots, the connected YAxis should also be removed. 

To reproduce the problem follow these steps:

1. Select line 1 in the grid and select checkbox to show plot 1.
2. Select line 2 in the grid and select checkbox to show plot 2.
3. Select line 3 in the grid and select checkbox to show plot 3.
4. Unselect checkbox in line 3 again to remove plot 3.
5. Select line 2 in the grid and unselect checkbox to remove plot 2.
6. Select line 1 in the grid and unselect checkbox to remove plot 1.

Expected Behvior: No plots visible.

Actual Behavior: Plot 1 does not remove from the list.
