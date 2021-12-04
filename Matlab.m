close all;
clear all;

fileID=fopen('tundi.txt');
Matrix=dlmread('tundi.txt');

%Cut coordinates related to button press
M = Matrix(42:end, :);

fclose(fileID);

%Get dimensions of the collected data
[dim1, dim2]=size(M);

Distances = zeros(dim1, 1);
regression_counter = 0;

%Count saccades and count regression movements
for n = 2:dim1-1
    x1 = M(n, :);
    x2 = M(n+1, :);
    Distances(n) = pdist2(x1, x2);
    if x2(1) < x1(1)
        regression_counter = regression_counter + 1;
    end
end


t = linspace(1,dim1, dim1);

%Plot of the coordinates
figure;
plot(M(:, 1), M(:, 2));
xlabel ('X');
ylabel ('Y');
title('Coordinates');
set(gca, 'YDir','reverse');
hold on;

%Plot of the distances
figure;
plot(t, Distances);
xlabel ('t');
ylabel ('Distance');
title('Distances between two gaze points');

%Counting the regressions and progressions, statistical data
distance_mean = mean(Distances);
distance_var = var(Distances);
big_progress = 0;
small_progress = 0;
big_regress = 0;
small_regress = 0;

for n = 1:dim1-1
    x1 = M(n, :);
    x2 = M(n+1, :);
    
    if x2(1) < x1(1) && Distances(n) > distance_mean
        big_regress = big_regress + 1;
    end
    
    if x2(1) > x1(1) && Distances(n) > distance_mean
        big_progress = big_progress + 1;
    end
    
    if x2(1) < x1(1) && Distances(n) < distance_mean
        small_regress = small_regress + 1;
    end
    
    if x2(1) > x1(1) && Distances(n) < distance_mean
        small_progress = small_progress + 1;
    end
end

%The difference between x coordinates
dx = diff(M(:, 1));

%Write the resoults into a txt
%writematrix(dx,'dx_tundi.txt','Delimiter',' ');
%writematrix(Distances,'d2_arpi.txt','Delimiter',' ');


%Separating the coordinates to lines
minim = min(M);
min_y = minim(2);

%The lines
one = zeros(1, 2);
two = zeros(1, 2);
three = zeros(1, 2);
four = zeros(1, 2);
five = zeros(1, 2);
six = zeros(1, 2);
seven = zeros(1, 2);
eight = zeros(1, 2);

for n = 1:dim1
    if M(n,2) > min_y && M(n,2) < (min_y+50)
        one = [one; M(n,:)];
    end
    if M(n,2) > (min_y+50) && M(n,2) < (min_y+100)
        two = [two; M(n,:)];
    end
    if M(n,2) > (min_y+100) && M(n,2) < (min_y+150)
        three = [three; M(n,:)];
    end
    if M(n,2) > (min_y+150) && M(n,2) < (min_y+200)
        four = [four; M(n,:)];
    end
    if M(n,2) > (min_y+200) && M(n,2) < (min_y+250)
        five = [five; M(n,:)];
    end
    if M(n,2) > (min_y+250) && M(n,2) < (min_y+300)
        six = [six; M(n,:)];
    end
    if M(n,2) > (min_y+300) && M(n,2) < (min_y+350)
        seven = [seven; M(n,:)];
    end
    if M(n,2) > (min_y+350) && M(n,2) < (min_y+400)
        eight = [eight; M(n,:)];
    end
end

%Plot the lines
figure;
plot(one(2:end, 1), one(2:end, 2));
hold on;
plot(two(2:end, 1), two(2:end, 2));
plot(three(2:end, 1), three(2:end, 2));
plot(four(2:end, 1), four(2:end, 2));
plot(five(2:end, 1), five(2:end, 2));
plot(six(2:end, 1), six(2:end, 2));
plot(seven(2:end, 1), seven(2:end, 2));
plot(eight(2:end, 1), eight(2:end, 2));
xlabel ('X');
ylabel ('Y');
title('Lines');
set(gca, 'YDir','reverse');
legend('line 1', 'line 2', 'line 3', 'line 4', 'line 5', 'line 6', 'line 7', 'line 8');

%Calculating the difference for each line
dx1 = diff(one(2:end, 1));
dx2 = diff(two(2:end, 1));
dx3 = diff(three(2:end, 1));
dx4 = diff(four(2:end, 1));
dx5 = diff(five(2:end, 1));
dx6 = diff(six(2:end, 1));
dx7 = diff(seven(2:end, 1));
dx8 = diff(eight(2:end, 1));

%Make the arrays the same lenght
L = [numel(dx1), numel(dx2), numel(dx3), numel(dx4), numel(dx5), numel(dx6), numel(dx7), numel(dx8)];
maxim = max(L);
dx1(end+1:maxim)=nan;
dx2(end+1:maxim)=nan;
dx3(end+1:maxim)=nan;
dx4(end+1:maxim)=nan;
dx5(end+1:maxim)=nan;
dx6(end+1:maxim)=nan;
dx7(end+1:maxim)=nan;
dx8(end+1:maxim)=nan;
lines =[dx1 dx2 dx3 dx4 dx5 dx6 dx7 dx8];
writematrix(lines,'line_tundi.txt','Delimiter',' ');