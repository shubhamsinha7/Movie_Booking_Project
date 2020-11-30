class Solution {
public:
    vector<int> revers(vector<int>a,int l,int h)
    {
        while(l<h)
        {
            int temp=a[l];
            a[l]=a[h];
            a[h]=temp;
            l++;
            h--;
        }
        return a;
    }
    void rotate(vector<int>& nums, int k) {
     int n=nums.size();
        k=k%n;
     nums=revers(nums,0,n-1);
     nums=revers(nums,k,n-1);
     nums=revers(nums,0,k-1);
    
    }
};