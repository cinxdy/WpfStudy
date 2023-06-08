# WPF Study (4.25~)

# Table of contents
1. [WPF HW1 : Making a sign up and a login page](#hw1)
2. [WPF HW2 : Expense Report Example](#hw2)
3. [WPF HW3 : Animated Button Example](#hw3)
4. [WPF HW4 : Making a Person Infomation List Page](#hw4)

*****

## WPF HW1 : Making a sign up and a login page <a name="hw1"></a>
### I've Learned..
- Understanding the meaning of UI/UX
- Making a simple sign-up page (similar to Naver sign up)

### My Question
- Q) How can I change the window title when the pages have their titles?
- A) In that case, we have to change one by one by using the page_loaded event handler.

### Review
- In view of UX, it would be better to use ComboBox to represent values more than 2.
- Every window should have minWidth and minHeight to prevent ruining the shapes when users change the window size.
- In view of UX, we have to think of label text. Label or NullText, which one is better to show the content to be typed in?

### Screenshot of a running process
<img src="SignUpPage/run1.png" width="400px"/>
<img src="SignUpPage/run2.png" width="400px"/>  

*****

## WPF HW2 : Expense Report Example <a name="hw2"></a>
### I've Learned..
- Grid, ListBox, StackPanel
- Label, Button
- DataGrid
- Event Handler (Click)
- Style, Template, Image  

- `Link` : [MicroSoft My First WPF Application](https://learn.microsoft.com/ko-kr/dotnet/desktop/wpf/getting-started/walkthrough-my-first-wpf-desktop-application)   

### Question
- Q) Where is the default directory of the Image source? When I load "watermark.png", where does the program try to find the image from?
- A) If I set the build mode of the image to "Resource", the compiler automatically merges the image to .dll file as a resource.  
  
- Q) What is Grid.ColumnSpan?
- A) ColumnSpan and RowSpan values are used to take up more than 1 cell.
For example, if I want to dispose of textblock in the grid (1,1) and (2,1), it can be Grid.Row=1, Grid.Column=1, and Grid.Column=2 because it takes up the 2 columns from column 1.
  
- Q) What is the difference between XPath and Path when binding?
- A) Normally binding something is done by Path, and XPath is for XML data binding. Also, there is some different syntax for querying.

### Screenshot of a running process
<img src="ExpenseIt/run1.png" width="400px"/>
<img src="ExpenseIt/run2.png" width="400px"/>

*****

### WPF HW3 : Animated Button Example <a name="hw3"></a>
### I've Learned..
- Style
- Animation
- Event Trigger

### Question
- Q) What is different between `<Style TartgetType="Button">` and `<Style TargetType="{x:Type Button}">`?
- A) These are the same but we usually prefer the second one.

- Q) Why do we use `Grid` inside of `ControlTemplate`? I just guess `ControlTemplate` should have only one child.
- A) Correct. Link : [Link](https://learn.microsoft.com/ko-kr/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-7.0)
  
- Q) What is different between `Trigger` and `EventTrigger`?
- A) We should know about 4 types of Trigger : Property Trigger, Event Trigger, Data Trigger, Multi Trigger. Link : [Link](https://just-my-blog.tistory.com/48)

### Screenshot of a running process
<img src="AnimatedButton/run.png" width="400px"/>

*****

## WPF HW4 : Making a Person Infomation List Page <a name="hw4"></a>
### I've Learned..
- using MVVM Pattern
- Modal

### Question
- Q) Is it a good pattern that model depends on a ViewModel? If not, should a viewModel have models as a member instance?
- A) MVVM Pattern means Model-View-ViewModel but sometimes a Model can work like a ViewModel in practice. So PersonModel can be considered as a view model which represents one person's info.
   
- Q) Do I only have to make DelegateCommand in order to allocate any method to ICommand? Or is there any solution or library to use something like DelegateCommand?
- A) You should make your own DelegateCommand class that inherits an ICommand interface. Of course there are many examples online.

- Q) How can modal send any data to their parent windows?
- A) In modal, no way to send data directly to the parent window by returning data value exists. Instead, you can determine the return status value as true or false, so that you can check whether we should get a data or not.

``` C#
// Child Modal SignUpModal.cs
void btn_close()
{
    // just close
    DialogResult = false;
}

void btn_submit()
{
    // save the data and close
    this.newPerson = DataContext as PersonModel;
    DialogResult = true;
}
```

``` C#
// Parent Window PersonViewModel.cs
private void Add()
{
    var modal = new SignUpModal();
    if (modal.ShowDialog() == false)
    {
        // just close
        MessageBox.Show("No Person!");
        return;
    }
    // get the data from modal
    _persons.Add(modal.newPerson);
}
```

- Q) Can I use string format in view (xaml) or is it better to do this in viewModel when showing data?
- A) What you choose is entirely up to you. But in this case, formatting string values in view would be better in my opinion.

### Screenshot of a running process
<img src="PersonList/run.png" width="800px"/>

*****
